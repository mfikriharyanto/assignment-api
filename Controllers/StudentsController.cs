using Assignment.Api.Data;
using Assignment.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StudentsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var students = await _dbContext.Students.ToListAsync();
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Student student)
    {
        if (student == null)
            return BadRequest();

        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = student.Id }, student);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Student student)
    {
        var existingStudent = await _dbContext.Students.FindAsync(id);

        if (existingStudent == null)
            return NotFound();

        existingStudent.Name = student.Name;
        existingStudent.Npm = student.Npm;

        _dbContext.Students.Update(existingStudent);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
            return NotFound();

        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}