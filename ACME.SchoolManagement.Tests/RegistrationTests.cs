using ACME.SchoolManagement.Domain;

namespace ACME.SchoolManagement.Tests;

public class RegistrationTests {
  [Fact]
  public void Registration_CreateValidRegistration_ShouldSucceed()
  {
    // Arrange
    var student = TestHelpers.CreateStudent("Leonardo Illanez", 20);
    var course = TestHelpers.CreateCourse("Matemática 101", 100m, new DateTime(2024, 2, 1), new DateTime(2024, 12, 31));

    // Act
    var registration = new Registration(student, course);

    // Assert
    Assert.NotNull(registration);
    Assert.Same(student, registration.Student);
    Assert.Same(course, registration.Course);
    Assert.False(registration.IsPaid);
    Assert.Contains(registration, course.Registrations);
  }

  [Fact]
  public void MarkAsPaid_RegistrationNotPaid_ShouldMarkAsPaid()
  {
    // Arrange
    var student = TestHelpers.CreateStudent("Leonardo Illanez", 20);
    var course = TestHelpers.CreateCourse("Matemática 101", 100m, new DateTime(2024, 2, 1), new DateTime(2024, 12, 31));
    var registration = new Registration(student, course);

    // Act
    registration.MarkAsPaid();

    // Assert
    Assert.True(registration.IsPaid);
  }

}