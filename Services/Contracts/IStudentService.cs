using Assignment.Api.Models;

namespace Assignment.Api.Services.Contracts;

public interface IStudentService
{
    Task<IEnumerable<Student>> Get();
    Task<Student?> Get(int id);
    void Create(Student student);
    void Update(Student student);
    void Delete(Student student);
}