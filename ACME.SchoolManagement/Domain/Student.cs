namespace ACME.SchoolManagement.Domain;

public class Student
{
  public Guid Id { get; private set; }
  public string Name { get; private set; } = null!;
  public int Age { get; private set; }

  public static Student Create(string name, int age) {
    EnsureValidAge(age);
    
    return new Student {
      Id = Guid.NewGuid(),
      Name = name,
      Age = age
    };
  }

  private static void EnsureValidAge(int age) {
    const int minimumAge = 18;
    if (age < minimumAge) {
      throw new InvalidOperationException($"El estudiante debe tener al menos {minimumAge} años.");
    }
  }
}