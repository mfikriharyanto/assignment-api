using Assignment.Api.Data;
using Assignment.Api.Models;
using Assignment.Api.Models.Dtos.Incoming;
using Assignment.Api.Models.Dtos.Outgoing;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public StudentsController(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var students = await _dbContext.Students.ToListAsync();
        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
        return Ok(studentsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
            return NotFound();

        var studentDto = _mapper.Map<StudentDto>(student);
        return Ok(studentDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post(CreateStudentDto createStudentDto)
    {
        var student = _mapper.Map<Student>(createStudentDto);
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();

        var studentDto = _mapper.Map<StudentDto>(student);
        return CreatedAtAction("Get", new { id = studentDto.Id }, studentDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateStudentDto updateStudentDto)
    {
        var student = await _dbContext.Students.FindAsync(id);

        if (student == null)
            return NotFound();

        student = _mapper.Map(updateStudentDto, student);
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
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