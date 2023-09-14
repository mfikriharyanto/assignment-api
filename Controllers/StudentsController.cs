using Assignment.Api.Models;
using Assignment.Api.Models.Dtos.Incoming;
using Assignment.Api.Models.Dtos.Outgoing;
using Assignment.Api.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStudentService _service;

    public StudentsController(IMapper mapper, IStudentService service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var students = await _service.Get();
        var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
        return Ok(studentsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _service.Get(id);

        if (student == null)
            return NotFound();

        var studentDto = _mapper.Map<StudentDto>(student);
        return Ok(studentDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Post(CreateStudentDto createStudentDto)
    {
        var student = _mapper.Map<Student>(createStudentDto);
        _service.Create(student);

        var studentDto = _mapper.Map<StudentDto>(student);
        return CreatedAtAction("Get", new { id = studentDto.Id }, studentDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateStudentDto updateStudentDto)
    {
        var student = await _service.Get(id);

        if (student == null)
            return NotFound();

        student = _mapper.Map(updateStudentDto, student);
        _service.Update(student);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _service.Get(id);

        if (student == null)
            return NotFound();

        _service.Delete(student);

        return NoContent();
    }
}