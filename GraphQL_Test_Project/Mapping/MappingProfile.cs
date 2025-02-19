using AutoMapper;
using GraphQL_Test_Project.Dtos;
using GraphQL_Test_Project.Schema.Mutations;
using GraphQL_Test_Project.Schema.Queries;

namespace GraphQL_Test_Project.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDTO, CourseType>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

            CreateMap<InstructorDTO, InstructorType>();
            CreateMap<StudentDTO, StudentType>();

            CreateMap<CourseType, CourseDTO>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students));

            CreateMap<InstructorType, InstructorDTO>();
            CreateMap<StudentType, StudentDTO>();

            CreateMap<CourseInputType, CourseDTO>();
            CreateMap<CourseDTO, CourseResult>();
        }
    }
}
