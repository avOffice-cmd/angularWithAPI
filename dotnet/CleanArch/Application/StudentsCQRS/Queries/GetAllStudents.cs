using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentsCQRS.Queries
{
    public class GetAllStudents : IRequest<List<ShowStudentDTO>> {}

    public class GetAllStudents_Handler : IRequestHandler<GetAllStudents, List<ShowStudentDTO>>
    {
        private readonly IApplicationDBContext _context;
        public GetAllStudents_Handler(IApplicationDBContext _applicationDBContext)
        {
            _context = _applicationDBContext;
        }

        public async Task<List<ShowStudentDTO>> Handle(GetAllStudents request, CancellationToken cancellationToken)
        {
            var allStudents = await _context.Students.ToListAsync();

            var showAllStudents = allStudents.Select(student => new ShowStudentDTO
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentAge = student.StudentAge,
                StudentEmail = student.StudentEmail,
                //CreatedBy = student.CreatedBy,
                LastModifiedDate = student.LastModifiedDate,
                LastModifiedBy = student.LastModifiedBy,
                //CreatedDate = student.CreatedDate,
                IsActive = student.IsActive

            }).ToList();

            return showAllStudents;
        }
    }
}
