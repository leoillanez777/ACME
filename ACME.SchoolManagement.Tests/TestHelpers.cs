using ACME.SchoolManagement.Domain;

namespace ACME.SchoolManagement.Tests;

public static class TestHelpers
{
  public static Student CreateStudent(string name, int age)
  {
    return Student.Create(name, age);
  }

  public static Course CreateCourse(string name, decimal registrationFee, DateTime startDate, DateTime endDate)
  {
    return Course.Create(name, registrationFee, startDate, endDate);
  }
}