using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Interfaces;
using ACME.SchoolManagement.Middleware;
using ACME.SchoolManagement.Services;
using Moq;

namespace ACME.SchoolManagement.Tests;

public class RegistrationServiceTests
{
  [Fact]
  public void RegisterStudentForCourse_ValidData_ShouldSucceed() {
    // Arrange
    var student = TestHelpers.CreateStudent("Leonardo Illanez", 20);
    var course = TestHelpers.CreateCourse("Matemática 101", 100m, new DateTime(2024, 2, 1), new DateTime(2024, 12, 31));
    var paymentAmount = course.RegistrationFee;

    var mockPaymentGateway = new Mock<IPaymentGateway>();
    mockPaymentGateway.Setup(pg => pg.ProcessPayment(paymentAmount, course)).Returns(true);

    var mockStudentRepository = new Mock<IRepository<Student>>();
    mockStudentRepository.Setup(r => r.GetById(student.Id)).Returns(student);

    var mockCourseRepository = new Mock<IRepository<Course>>();
    mockCourseRepository.Setup(r => r.GetById(course.Id)).Returns(course);

    var mockRegistrationRepository = new Mock<IRepository<Registration>>();

    var registrationService = new RegistrationService(
        mockPaymentGateway.Object,
        mockStudentRepository.Object,
        mockCourseRepository.Object,
        mockRegistrationRepository.Object);

    // Act
    registrationService.RegisterStudentForCourse(student.Id, course.Id, paymentAmount);

    // Assert
    mockRegistrationRepository.Verify(r => r.Add(It.IsAny<Registration>()), Times.Once);
  }

  [Fact]
  public void RegisterStudentForCourse_CourseNotFound_ShouldThrowCourseNotFoundException() {
    // Arrange
    var student = TestHelpers.CreateStudent("John Doe", 20);
    var course = TestHelpers.CreateCourse("Math 101", 100m, new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
    var paymentAmount = course.RegistrationFee;

    var mockPaymentGateway = new Mock<IPaymentGateway>();
    var mockStudentRepository = new Mock<IRepository<Student>>();
    mockStudentRepository.Setup(r => r.GetById(student.Id)).Returns(student);
    var mockCourseRepository = new Mock<IRepository<Course>>();
        _ = mockCourseRepository.Setup(r => r.GetById(course.Id)).Returns((Course)null!);
    var mockRegistrationRepository = new Mock<IRepository<Registration>>();

    var registrationService = new RegistrationService(
        mockPaymentGateway.Object,
        mockStudentRepository.Object,
        mockCourseRepository.Object,
        mockRegistrationRepository.Object);

    // Act & Assert
    var exception = Assert.Throws<CourseNotFoundException>(() =>
        registrationService.RegisterStudentForCourse(student.Id, course.Id, paymentAmount));
    Assert.Equal($"No se encontró un curso con el ID '{course.Id}'.", exception.Message);
  }

  [Fact]
  public void RegisterStudentForCourse_PaymentFailure_ShouldThrowPaymentFailedException() {
    // Arrange
    var student = TestHelpers.CreateStudent("John Doe", 20);
    var course = TestHelpers.CreateCourse("Math 101", 100m, new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
    var paymentAmount = course.RegistrationFee;

    var mockPaymentGateway = new Mock<IPaymentGateway>();
    mockPaymentGateway.Setup(pg => pg.ProcessPayment(paymentAmount, course)).Returns(false);
    var mockStudentRepository = new Mock<IRepository<Student>>();
    mockStudentRepository.Setup(r => r.GetById(student.Id)).Returns(student);
    var mockCourseRepository = new Mock<IRepository<Course>>();
    mockCourseRepository.Setup(r => r.GetById(course.Id)).Returns(course);
    var mockRegistrationRepository = new Mock<IRepository<Registration>>();

    var registrationService = new RegistrationService(
        mockPaymentGateway.Object,
        mockStudentRepository.Object,
        mockCourseRepository.Object,
        mockRegistrationRepository.Object);

    // Act & Assert
    var exception = Assert.Throws<PaymentFailedException>(() =>
        registrationService.RegisterStudentForCourse(student.Id, course.Id, paymentAmount));
    Assert.Equal("El pago no fue exitoso. No se pudo realizar la matrícula.", exception.Message);
  }

  [Fact]
  public void RegisterStudentForCourse_InsufficientPaymentAmount_ShouldThrowInsufficientPaymentException()
  {
    // Arrange
    var student = TestHelpers.CreateStudent("John Doe", 20);
    var course = TestHelpers.CreateCourse("Math 101", 100m, new DateTime(2023, 9, 1), new DateTime(2023, 12, 31));
    var paymentAmount = course.RegistrationFee - 1m; // Pago insuficiente

    var mockPaymentGateway = new Mock<IPaymentGateway>();
    var mockStudentRepository = new Mock<IRepository<Student>>();
    mockStudentRepository.Setup(r => r.GetById(student.Id)).Returns(student);
    var mockCourseRepository = new Mock<IRepository<Course>>();
    mockCourseRepository.Setup(r => r.GetById(course.Id)).Returns(course);
    var mockRegistrationRepository = new Mock<IRepository<Registration>>();

    var registrationService = new RegistrationService(
        mockPaymentGateway.Object,
        mockStudentRepository.Object,
        mockCourseRepository.Object,
        mockRegistrationRepository.Object);

    // Act & Assert
    var exception = Assert.Throws<InsufficientPaymentException>(() =>
        registrationService.RegisterStudentForCourse(student.Id, course.Id, paymentAmount));
    Assert.Equal($"El monto de pago ({paymentAmount}) no cubre el costo de la matrícula ({course.RegistrationFee}).", exception.Message);
  }

  [Fact]
  public void GetEnrollments_ValidDateRange_ShouldReturnCorrectRegistrations()
  {
    // Arrange
    var student1 = TestHelpers.CreateStudent("Leonardo Illanez", 20);
    var student2 = TestHelpers.CreateStudent("Matias Suarez", 22);

    var course1 = TestHelpers.CreateCourse("Matemática 101", 100m, new DateTime(2023, 9, 1), new DateTime(2024, 12, 31));
    var course2 = TestHelpers.CreateCourse("Química 101", 150m, new DateTime(2023, 10, 1), new DateTime(2024, 1, 31));
    var course3 = TestHelpers.CreateCourse("Física 101", 120m, new DateTime(2024, 1, 1), new DateTime(2024, 4, 30));

    var registration1 = new Registration(student1, course1);
    var registration2 = new Registration(student2, course2);
    var registration3 = new Registration(student1, course3);

    var mockRegistrationRepository = new Mock<IRepository<Registration>>();
    mockRegistrationRepository.Setup(r => r.GetAll()).Returns([registration1, registration2, registration3]);

    var registrationService = new RegistrationService(
        null,
        null,
        null,
        mockRegistrationRepository.Object);

    var from = new DateTime(2023, 9, 1);
    var to = new DateTime(2023, 12, 31);

    // Act
    var enrollments = registrationService.GetEnrollments(from, to);

    // Assert
    Assert.Equal(2, enrollments.Count());
    Assert.Contains(registration1, enrollments);
    Assert.Contains(registration2, enrollments);
    Assert.DoesNotContain(registration3, enrollments);
  }
}