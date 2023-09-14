using System.ComponentModel.DataAnnotations;

namespace Assignment.Api.Models.Dtos.Incoming;

public record UpdateStudentDto(
    [Required]
    string Name
);