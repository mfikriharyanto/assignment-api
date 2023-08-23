namespace Assignment.Api.Models.Dtos.Incoming;

public record SignInUserDto(
    string Email,
    string Password
);