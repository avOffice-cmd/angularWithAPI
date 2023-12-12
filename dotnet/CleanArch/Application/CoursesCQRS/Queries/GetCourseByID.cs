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
    public class GetCourseByID : IRequest<Course>
    {
        public int CourseID { get; set; }
    }


    public class GetCourseByIDHandler : IRequestHandler<GetCourseByID, Course>
    {
        private readonly IApplicationDBContext _context;
        public GetCourseByIDHandler(IApplicationDBContext _applicationDBContext)
        {
            _context = _applicationDBContext;
        }

        public async Task<Course> Handle(GetCourseByID request, CancellationToken cancellationToken)
        {
            var allCourses = await _context.Courses.ToListAsync();

            var gotCourse = allCourses.FirstOrDefault(c => c.CourseId == request.CourseID);

            return gotCourse;
        }
    }
}
