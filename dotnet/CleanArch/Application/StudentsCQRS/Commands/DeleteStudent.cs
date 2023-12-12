using Application.CoursesCQRS.Queries;
using Application.StudentsCQRS.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentsCQRS.Commands
{
    public class DeleteStudent : IRequest<string>
    {
        public int StudentID { get; set; }
    }


    public class DeleteStudent_Handler : IRequestHandler<DeleteStudent, string>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMediator mediator;

        public DeleteStudent_Handler(IApplicationDBContext _applicationDBContext, IMediator _mediator)
        {
            _context = _applicationDBContext;
            mediator = _mediator;
        }

        public async Task<string> Handle(DeleteStudent request, CancellationToken cancellationToken)
        {
            //var student = await mediator.Send(new GetStudentByID { StdId = request.StudentID });

            var student = await _context.Students.FirstOrDefaultAsync(st => st.StudentID == request.StudentID);

            if (student != null)
            {
                if (student.IsActive == false)
                {
                    return "Deleted already!";
                } 

                student.IsActive = false;
                student.LastModifiedBy = "Admin";
                student.LastModifiedDate = DateTime.Now;

                int result = await _context.SaveChangesAsync(cancellationToken);
                if(result > 0)
                {
                    return "Student Deleted Successfully";
                }
                else { return "Changes not saved!"; }

            }

            return "Student Not found";
        }
    }
}
