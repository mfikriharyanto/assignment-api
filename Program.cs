using Assignment.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("AssignmentContext");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Assignment API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
