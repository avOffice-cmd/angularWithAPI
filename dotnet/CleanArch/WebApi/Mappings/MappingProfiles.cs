using Application;
using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Serilog;
using System.Buffers.Text;

namespace WebApi.Mappings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            #region ADD STUDENT
            // add student
            //CreateMap<AddStudentDTO, Student>()
            //    .ForMember(dest => dest.StudentName,
            //                opt => opt.MapFrom(src => src.StudentFullName)
            //    );

            CreateMap<AddStudentDTO, Student>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.StudentFullName))

                .ForMember(dest => dest.StudentCity, opt => opt.MapFrom(src => GetCityFromCityOrCountry(src.StudentCityOrCountry)))
                .ForMember(dest => dest.StudentCountry, opt => opt.MapFrom(src => GetCountryFromCityOrCountry(src.StudentCityOrCountry)))
                
                .ForMember(dest => dest.AgeGroupCount, opt => opt.MapFrom<AgeGroupCounter>());



            #endregion



            #region GET STUDENT
            // get student
            CreateMap<Student, ShowStudentDTO>()
                    .ForMember(dest => dest.CreatedDetails,
                                opt => opt.MapFrom(src => $"{src.CreatedBy} - {src.CreatedDate}"));

            #endregion


            // add course
            CreateMap<AddCourseDTO, Course>()
                 .ForMember(dest => dest.CourseName,
                          opt => opt.MapFrom<AddCourseResolver>());


        }

        private string GetCityFromCityOrCountry(string cityOrCountry)
        {
            return cityOrCountry?.Split(',')[0];
        }

        private string GetCountryFromCityOrCountry(string cityOrCountry)
        {
            return cityOrCountry?.Split(',')[0];
        }



      
    }

    public class AgeGroupCounter : IValueResolver<AddStudentDTO, Student, string>
    {
        private readonly IApplicationDBContext _context;

        public AgeGroupCounter(IApplicationDBContext context)
        {
            _context = context;
        }

        public string Resolve(AddStudentDTO source, Student destination, string destMember, ResolutionContext context)
        {
            
            var studentCount = _context.Students.Count(s => s.StudentAge == source.StudentAge);

            return $"{source.StudentAge} - {studentCount + 1}";
        }
    }


    class AddCourseResolver : IValueResolver<AddCourseDTO, Course, string>
    {
        private readonly IApplicationDBContext _context;
        public AddCourseResolver(IApplicationDBContext context)
        {
            _context = context;
        }
        public string Resolve(AddCourseDTO source, Course destination, string destMember, ResolutionContext context)
        {
            var student = _context.Students.ToList();
            var gotStudent = student.FirstOrDefault(s => s.StudentID == source.StudentId);

            if (gotStudent != null)
            {
                return $"{source.CourseName}_{gotStudent.StudentName}";
            }

            return source.CourseName;
        }
    }



}


//In this example, the AfterMap method is used to perform additional
//logic after the basic mapping has taken place. It retrieves the student
// name from the database based on the provided StudentId and concatenates 
//it with the CourseName.This avoids the need for a separate value resolver class.



