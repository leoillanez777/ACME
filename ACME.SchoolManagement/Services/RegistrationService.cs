using System.Security.Authentication;
using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Interfaces;
using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Services;

public class RegistrationService {
  private readonly IPaymentGateway _paymentGateway;
  private readonly IRepository<Student> _studentRepository;
  private readonly IRepository<Course> _courseRepository;
  private readonly IRepository<Registration> _registrationRepository;

  public RegistrationService(IPaymentGateway paymentGateway,
    IRepository<Student> studentRepository, 
    IRepository<Course> courseRepository, 
    IRepository<Registration> registrationRepository
  ) {
    _paymentGateway = paymentGateway;
    _studentRepository = studentRepository;
    _courseRepository = courseRepository;
    _registrationRepository = registrationRepository;
  }

  public void RegisterStudentForCourse(Guid studentId, Guid courseId, decimal paymentAmount) {
    var student = _studentRepository.GetById(studentId);
    var course = _courseRepository.GetById(courseId);
    if (student == null)
      throw new StudentNotFoundException($"No se encontró un estudiante con el ID '{studentId}'.");

    if (course == null)
      throw new CourseNotFoundException($"No se encontró un curso con el ID '{courseId}'.");

    if (paymentAmount < course.RegistrationFee)
      throw new InsufficientPaymentException($"El monto de pago ({paymentAmount}) no cubre el costo de la matrícula ({course.RegistrationFee})."); 

    bool paymentSuccess = _paymentGateway.ProcessPayment(paymentAmount, course);

    if (!paymentSuccess)
        throw new PaymentFailedException("El pago no fue exitoso. No se pudo realizar la matrícula.");
    
    var registration = new Registration(student, course);
    registration.MarkAsPaid();
    _registrationRepository.Add(registration);
  }

  public IEnumerable<Registration> GetEnrollments(DateTime from, DateTime to) {
    return _registrationRepository.GetAll()
            .Where(r => (r.Course.StartDate >= from && r.Course.StartDate <= to) ||
                    (r.Course.EndDate >= from && r.Course.EndDate <= to));
  }
}