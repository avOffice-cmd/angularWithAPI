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

namespace Application.StudentsCQRS.Commands
{
    public class UpdateStudent : IRequest<string>
    {
        public UpdateStudentDTO UpdateStudentDTO { get; set; }
    }

    public class UpdateStudent_Handler : IRequestHandler<UpdateStudent, string>
    {
        private readonly IApplicationDBContext _context;

        public UpdateStudent_Handler(IApplicationDBContext _applicationDBContext)
        {
            _context = _applicationDBContext;
        }

        public async Task<string> Handle(UpdateStudent request, CancellationToken cancellationToken)
        {

            var allStudents = await _context.Students
                                        .AsNoTracking().ToListAsync();

            var student = allStudents
                            .FirstOrDefault(s => s.StudentID == request.UpdateStudentDTO.StudentID);

            if (student != null)
            {
                if (student.IsActive == false)
                {
                    return "Student Not found";
                }

                Student updatedStudentObject = new Student
                {
                    StudentID = request.UpdateStudentDTO.StudentID,
                    StudentName = request.UpdateStudentDTO.StudentName,
                    StudentAge = request.UpdateStudentDTO.StudentAge,
                    StudentEmail = request.UpdateStudentDTO.StudentEmail,
                    AgeGroupCount = request.UpdateStudentDTO.AgeGroupCount,
                    StudentAddress = request.UpdateStudentDTO.StudentAddress,
                    StudentCity = request.UpdateStudentDTO.StudentCity,
                    StudentCountry = request.UpdateStudentDTO.StudentCountry,
                    CreatedBy = student.CreatedBy,
                    CreatedDate = student.CreatedDate,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = "Admin",
                    IsActive = student.IsActive
                };


                _context.Students.Update(updatedStudentObject);
                await _context.SaveChangesAsync(cancellationToken);

                return "student Updated Successfully!";

            }

            return "student not found";
        }
    }
}
