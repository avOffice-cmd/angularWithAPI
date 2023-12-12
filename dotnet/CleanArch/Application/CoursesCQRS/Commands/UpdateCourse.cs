using Application.DTOs.CourseDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class UpdateCourse : IRequest<string>
    {
        public UpdateCourseDTO UpdateCourseDTO { get; set; }
    }

    public class UpdateCourse_Handler : IRequestHandler<UpdateCourse, string>
    {
        private readonly IApplicationDBContext _context;

        public UpdateCourse_Handler(IApplicationDBContext _applicationDBContext)
        {
            _context = _applicationDBContext;
        }

        public async Task<string> Handle(UpdateCourse request, CancellationToken cancellationToken)
        {

            var gotAllCourses = await _context.Courses
                                        .AsNoTracking()
                                        .ToListAsync();

            var course = gotAllCourses
                            .FirstOrDefault(c => c.CourseId == request.UpdateCourseDTO.CourseId);

            if (course != null)
            {
                if (course.IsActive == false)
                {
                    return "Course Not found";
                }

                Course updatedCourseObject = new Course
                {
                    CourseId = request.UpdateCourseDTO.CourseId,
                    CourseName = request.UpdateCourseDTO.CourseName,
                    StudentID = request.UpdateCourseDTO.StudentId,
                    CreatedBy = course.CreatedBy,
                    CreatedDate = course.CreatedDate,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = "Admin",
                    IsActive = course.IsActive
                };


                _context.Courses.Update(updatedCourseObject);
                await _context.SaveChangesAsync(cancellationToken);


                return "Course Updated Succesfully";

            }

            return "Course not found";
        }
    }
}
