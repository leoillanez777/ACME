using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Domain;

public class Course {
  public Guid Id { get; private set; }
  public string Name { get; private set; } = null!;
  public decimal RegistrationFee { get; private set; }
  public DateTime StartDate { get; private set; }
  public DateTime EndDate { get; private set; }
  private readonly List<Registration> _registrations = [];

  public static Course Create(string name, decimal registrationFee, DateTime startDate, DateTime endDate) {
    EnsureValidDates(startDate, endDate);

    return new Course
    {
      Id = Guid.NewGuid(),
      Name = name,
      RegistrationFee = registrationFee,
      StartDate = startDate,
      EndDate = endDate
    };
  }

  public IReadOnlyCollection<Registration> Registrations => _registrations.AsReadOnly();

  public void AddRegistration(Registration registration) {
    _registrations.Add(registration);
  }

  private static void EnsureValidDates(DateTime startDate, DateTime endDate) {
    if (endDate <= startDate) {
      throw new InvalidCourseDateRangeException("La fecha de finalización debe ser posterior a la fecha de inicio.");
    }
  }
}