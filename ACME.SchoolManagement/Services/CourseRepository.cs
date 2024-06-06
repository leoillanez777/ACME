using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Interfaces;
using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Services;

public class CourseRepository : IRepository<Course> {
  private readonly List<Course> _courses = [];

  public IEnumerable<Course> GetAll() => _courses;

  public Course GetById(Guid id) {
    EnsureCourseExists(id);

    return _courses.First(c => c.Id == id);
  }

  public void Add(Course course) {
    EnsureCourseDoesNotExist(course.Id);
    _courses.Add(course);
  }

  public void Remove(Course course) {
    EnsureCourseExists(course.Id);
    _courses.Remove(course);
  }

  private void EnsureCourseDoesNotExist(Guid id) {
    if (_courses.Any(c => c.Id == id)) {
      throw new DuplicateCourseException($"Ya existe un curso con el ID '{id}'.");
    }
  }

  private void EnsureCourseExists(Guid id) {
    if (!_courses.Any(c => c.Id == id)) {
      throw new CourseNotFoundException($"No se encontró un curso con el ID '{id}'.");
    }
  }
}