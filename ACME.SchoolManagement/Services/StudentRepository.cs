using ACME.SchoolManagement.Domain;
using ACME.SchoolManagement.Interfaces;
using ACME.SchoolManagement.Middleware;

namespace ACME.SchoolManagement.Services;

public class StudentRepository : IRepository<Student> {
  private readonly List<Student> _students = [];
  
  public IEnumerable<Student> GetAll() => _students;

  public Student GetById(Guid id)
  {
    EnsureStudentExists(id);
    return _students.Single(s => s.Id == id);
  }

  public void Add(Student student) {
    EnsureStudentDoesNotExist(student.Id);
    _students.Add(student);
  }

  public void Remove(Student student) {
    EnsureStudentExists(student.Id);
    _students.Remove(student);
  }

  private void EnsureStudentDoesNotExist(Guid id)
  {
    if (_students.Any(s => s.Id == id)) {
      throw new DuplicateStudentException($"Ya existe un estudiante con el ID '{id}'.");
    }
  }

  private void EnsureStudentExists(Guid id)
  {
    if (!_students.Any(s => s.Id == id)){
      throw new StudentNotFoundException($"No se encontró un estudiante con el ID '{id}'.");
    }
  }
}