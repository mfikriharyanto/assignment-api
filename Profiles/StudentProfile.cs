using Assignment.Api.Models;
using Assignment.Api.Models.Dtos.Incoming;
using Assignment.Api.Models.Dtos.Outgoing;
using AutoMapper;

namespace Assignment.Api.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>()
            .ForMember(dest => dest.Npm, opt => opt.Ignore());
        CreateMap<Student, StudentDto>();
    }
}