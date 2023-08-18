using Assignment.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        return Ok(Student.All());
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetStudent(string id)
    {
        Student? student = Student.All().Find(student => student.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> CreateStudent(Student student)
    {
        if (student == null)
            return BadRequest();

        var createdStudent = Student.Add(student);

        return CreatedAtAction(nameof(GetStudent), new { id = createdStudent.Id }, createdStudent);
    }
}