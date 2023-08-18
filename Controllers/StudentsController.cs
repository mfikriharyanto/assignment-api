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
}