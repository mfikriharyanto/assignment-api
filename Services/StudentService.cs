using Assignment.Api.Data;
using Assignment.Api.Models;
using Assignment.Api.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Api.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _dbContext;

    public StudentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Student>> Get()
    {
        return await _dbContext.Students.ToListAsync();
    }

    public async Task<Student?> Get(int id)
    {
        return await _dbContext.Students.FindAsync(id);
    }

    public async void Create(Student student)
    {
        await _dbContext.Students.AddAsync(student);
        await _dbContext.SaveChangesAsync();
    }

    public async void Update(Student student)
    {
        _dbContext.Students.Update(student);
        await _dbContext.SaveChangesAsync();
    }

    public async void Delete(Student student)
    {
        _dbContext.Students.Remove(student);
        await _dbContext.SaveChangesAsync();
    }
}