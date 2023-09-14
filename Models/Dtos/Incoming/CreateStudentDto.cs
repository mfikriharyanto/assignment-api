using System.ComponentModel.DataAnnotations;

namespace Assignment.Api.Models.Dtos.Incoming;

public record CreateStudentDto(
    [Required]
    string Name,

    [Required]
    string Npm
);