namespace ACME.SchoolManagement.Middleware;

public class InvalidCourseDateRangeException : Exception {
  public InvalidCourseDateRangeException() { }

  public InvalidCourseDateRangeException(string message) : base(message) { }

  public InvalidCourseDateRangeException(string message, Exception innerException) : base(message, innerException) { }
}

public class DuplicateCourseException : Exception {
  public DuplicateCourseException() { }

  public DuplicateCourseException(string message) : base(message) { }

  public DuplicateCourseException(string message, Exception innerException) : base(message, innerException) { }
}

public class CourseNotFoundException : Exception {
  public CourseNotFoundException() { }

  public CourseNotFoundException(string message) : base(message) { }

  public CourseNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}