using Assignment.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(Student.All());
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        Student? student = Student.All().Find(student => student.Id == id);
        
        if (student == null)
            return NotFound();
        
        return Ok(student);
    }

    [HttpPost]
    public IActionResult Post(Student student)
    {
        if (student == null)
            return BadRequest();

        var createdStudent = Student.Add(student);

        return CreatedAtAction("Get", new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put(int id, Student student)
    {
        if (id != student.Id)
            return BadRequest();

        Student? updatedStudent = Student.All().Find(student => student.Id == id);
        
        if (updatedStudent == null)
            return NotFound();

        updatedStudent.Name = student.Name;
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        Student? student = Student.All().Find(student => student.Id == id);
        
        if (student == null)
            return NotFound();

        Student.Remove(student);
        return NoContent();
    }
}