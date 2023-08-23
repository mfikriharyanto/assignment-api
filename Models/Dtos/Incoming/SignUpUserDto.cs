namespace Assignment.Api.Models.Dtos.Incoming;

public record SignUpUserDto(
    string Name,
    string Email,
    string Password
);