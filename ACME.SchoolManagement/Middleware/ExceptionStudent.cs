namespace ACME.SchoolManagement.Middleware;

public class DuplicateStudentException : Exception {
  public DuplicateStudentException() { }

  public DuplicateStudentException(string message) : base(message) { }

  public DuplicateStudentException(string message, Exception innerException) : base(message, innerException) { }
}

public class StudentNotFoundException : Exception {
  public StudentNotFoundException() { }

  public StudentNotFoundException(string message) : base(message) { }

  public StudentNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}