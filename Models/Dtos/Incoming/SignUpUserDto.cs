using System.ComponentModel.DataAnnotations;

namespace Assignment.Api.Models.Dtos.Incoming;

public record SignUpUserDto(
    [Required]
    string Name,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(8)]
    string Password
);