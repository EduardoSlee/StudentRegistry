using AutoMapper;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students.Models;

namespace StudentRegistry.Services.Students.Mappings
{
    public class StudentsMappingProfile : Profile
    {
        public StudentsMappingProfile()
        {
            CreateMap<Student, StudentResult>()
                .ForMember(dest => dest.CreateDate, options => options.MapFrom(
                    student => student.CreateDate.ToString("dd/MM/yyyy")));
            CreateMap<StudentInput, Student>();
            CreateMap<Student, StudentReport>()
                .ForMember(dest => dest.SexDescription, options => options.MapFrom(
                    source => source.Sex ? "Male" : "Female"));
        }
    }
}
