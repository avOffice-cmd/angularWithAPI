using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.StudentDTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.StudentsCQRS.Commands
{

    public class AddStudent : IRequest<Student>
    {
        public AddStudentDTO _addStudentDTO { get; set; }
    }


    public class AddStudent_Handler : IRequestHandler<AddStudent, Student>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public AddStudent_Handler(IApplicationDBContext _applicationDBContext, IMapper mapper)
        {
            _context = _applicationDBContext;
            _mapper = mapper;
        }

        public async Task<Student> Handle(AddStudent request, CancellationToken cancellationToken)
        {

            //Student addStudent = new Student
            //{
            //    StudentName = request._addStudentDTO.StudentName,
            //    StudentAge = request._addStudentDTO.StudentAge,
            //    StudentEmail = request._addStudentDTO.StudentEmail,
            //    CreatedBy = "Admin",
            //    CreatedDate = DateTime.Now,
            //    IsActive = true
            //};

                    // destination->source


            var addStudent =  _mapper.Map<Student>(request._addStudentDTO);
                addStudent.CreatedDate = DateTime.Now;
                addStudent.CreatedBy = "Admin";
                addStudent.IsActive = true;


            _context.Students.Add(addStudent);

            await _context.SaveChangesAsync(cancellationToken);

            return addStudent;

        }
    }

}
