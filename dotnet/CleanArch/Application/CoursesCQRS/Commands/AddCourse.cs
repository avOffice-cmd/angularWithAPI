using Application.DTOs.CourseDTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class AddCourse : IRequest<Course>
    {
        public AddCourseDTO _addCourseDTO { get; set; }
    }


    public class AddCourse_Handler : IRequestHandler<AddCourse, Course>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public AddCourse_Handler(IApplicationDBContext _applicationDBContext, IMapper mapper)
        {
            _context = _applicationDBContext;
            _mapper = mapper;
        }

        public async Task<Course> Handle(AddCourse request, CancellationToken cancellationToken)
        {

            //Course addCourse = new Course
            //{
            //    CourseName = request._addCourseDTO.CourseName,
            //    StudentID = request._addCourseDTO.StudentId,
            //    CreatedBy = "Admin",
            //    CreatedDate = DateTime.Now,
            //    IsActive = true
            //};

            var addCourse =  _mapper.Map<Course>(request._addCourseDTO);
            _context.Courses.Add(addCourse);

            await _context.SaveChangesAsync(cancellationToken);

            return addCourse;

        }
    }
}
