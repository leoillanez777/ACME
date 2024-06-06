using ACME.SchoolManagement.Domain;

namespace ACME.SchoolManagement.Tests;

public class StudentTest
{
  [Fact]
  public void Create_ValidStudent_ShouldSucceed()
  {
    // Arrange
    var name = "John Doe";
    var age = 20;

    // Act
    var student = Student.Create(name, age);

    // Assert
    Assert.NotNull(student);
    Assert.Equal(name, student.Name);
    Assert.Equal(age, student.Age);
  }

  [Fact]
  public void Create_StudentUnderAge_ShouldThrowException()
  {
    // Arrange
    var name = "Jane Doe";
    var age = 17;

    // Act & Assert
    var exception = Assert.Throws<InvalidOperationException>(() =>
        Student.Create(name, age));
    Assert.Equal($"El estudiante debe tener al menos 18 años.", exception.Message);
  }
}