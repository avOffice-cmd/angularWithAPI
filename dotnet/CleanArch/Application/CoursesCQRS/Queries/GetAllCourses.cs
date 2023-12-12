using Application.DTOs.CourseDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Queries
{
    public class GetAllCourses : IRequest<List<ShowCourseDTO>>
    {

    }

    public class GetAllCoursesHandler : IRequestHandler<GetAllCourses, List<ShowCourseDTO>>
    {
        private readonly IApplicationDBContext _context;
        public GetAllCoursesHandler(IApplicationDBContext _applicationDBContext)
        {
            _context = _applicationDBContext;
        }

        public async Task<List<ShowCourseDTO>> Handle(GetAllCourses request, CancellationToken cancellationToken)
        {
            var allCourses = await _context.Courses.ToListAsync();

            var showAllCourses = allCourses.Select(course => new ShowCourseDTO
            {
                CourseId = course.CourseId,
                StudentId = course.StudentID,
                CourseName = course.CourseName,
                CreatedDate = course.CreatedDate,
                LastModifiedBy = course.LastModifiedBy,
                CreatedBy = course.CreatedBy,
                LastModifiedDate = course.LastModifiedDate,
                IsActive = course.IsActive

            }
            ).ToList();


            return showAllCourses;
        }
    }
}
