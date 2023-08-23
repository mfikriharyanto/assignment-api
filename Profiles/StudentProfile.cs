using Assignment.Api.Models;
using Assignment.Api.Models.Dtos.Incoming;
using Assignment.Api.Models.Dtos.Outgoing;
using AutoMapper;

namespace Assignment.Api.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<CreateStudentDto, Student>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Npm, opt => opt.MapFrom(src => src.Npm));

        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Npm, opt => opt.MapFrom(src => src.Npm));
    }
}