using Application.CoursesCQRS.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class DeleteCourse : IRequest<string>
    {
        public int CourseID { get; set; }
    }


    public class DeleteCourse_Handler : IRequestHandler<DeleteCourse, string>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMediator mediator;

        public DeleteCourse_Handler(IApplicationDBContext _applicationDBContext, IMediator _mediator)
        {
            _context = _applicationDBContext;
            mediator = _mediator;
        }

        public async Task<string> Handle(DeleteCourse request, CancellationToken cancellationToken)
        {

            var course = await mediator.Send(new GetCourseByID { CourseID = request.CourseID });


            if (course != null)
            {
                if (course.IsActive == false)
                {
                    return "Deleted already!";
                }

                course.LastModifiedBy = "Admin";
                course.LastModifiedDate = DateTime.Now;
                course.IsActive = false;


                await _context.SaveChangesAsync(cancellationToken);

                return "Course Deleted Successfuly";
            }

            return "Course not found";
        }
    }
}
