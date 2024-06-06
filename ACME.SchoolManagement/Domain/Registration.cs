namespace ACME.SchoolManagement.Domain;

public class Registration {
  public Guid Id { get; private set; }
  public Student Student { get; private set; }
  public Course Course { get; private set; }
  public bool IsPaid { get; private set; }

  public Registration(Student student, Course course)
  {
    Id = Guid.NewGuid();
    Student = student;
    Course = course;
    IsPaid = false;

    course.AddRegistration(this);
  }
  public void MarkAsPaid() {
    IsPaid = true;
  }
}