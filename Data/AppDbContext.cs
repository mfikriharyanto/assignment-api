using Assignment.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students{ get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}