using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Tests;

public class CourseTest {
  
  [Fact]
  public void Create_ValidCourse_ShouldSucceed()
  {
    // Arrange
    var name = "Matemática 101";
    var registrationFee = 100m;
    var startDate = new DateTime(2024, 2, 1);
    var endDate = new DateTime(2024, 12, 31);

    // Act
    var course = Course.Create(name, registrationFee, startDate, endDate);

    // Assert
    Assert.NotNull(course);
    Assert.Equal(name, course.Name);
    Assert.Equal(registrationFee, course.RegistrationFee);
    Assert.Equal(startDate, course.StartDate);
    Assert.Equal(endDate, course.EndDate);
  }

  [Fact]
  public void Create_EndDateBeforeStartDate_ShouldThrowException()
  {
    // Arrange
    var name = "Matemática 101";
    var registrationFee = 100m;
    var startDate = new DateTime(2024, 2, 1);
    var endDate = new DateTime(2023, 8, 31);

    // Act & Assert
    var exception = Assert.Throws<InvalidCourseDateRangeException>(() =>
        Course.Create(name, registrationFee, startDate, endDate));
    Assert.Equal("La fecha de finalización debe ser posterior a la fecha de inicio.", exception.Message);
  }

}