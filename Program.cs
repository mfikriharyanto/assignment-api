using Assignment.Api.Configurations;
using Assignment.Api.Data;
using Assignment.Api.Services;
using Assignment.Api.Services.Contracts;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("AssignmentContext"))
    );

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddJwtAuthorization();
builder.Services.AddDefaultIdentity<IdentityUser>(
    options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Assignment API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
