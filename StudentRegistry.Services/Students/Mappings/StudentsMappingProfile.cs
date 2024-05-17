using AutoMapper;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students.Models;

namespace StudentRegistry.Services.Students.Mappings
{
    public class StudentsMappingProfile : Profile
    {
        public StudentsMappingProfile()
        {
            CreateMap<Student, StudentResult>();
            CreateMap<StudentInput, Student>();
            CreateMap<Student, StudentReport>()
                .ForMember(dest => dest.SexDescription, options => options.MapFrom(
                    source => source.Sex ? "Male" : "Female"));
        }
    }
}
