using Application.DTOs.StudentDTOs;
using AutoMapper;
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
    public class GetStudentByID : IRequest<ShowStudentDTO>
    {
        public int StdId { get; set; }
    }


    public class GetStudentByID_Handler : IRequestHandler<GetStudentByID, ShowStudentDTO>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public GetStudentByID_Handler(IApplicationDBContext applicationDBContext, IMapper mapper)
        {
            _context = applicationDBContext;
            _mapper = mapper;
        }


        //public async Task<Student> Handle(GetStudentByID request, CancellationToken cancellationToken)
        //{
        //    var allStudents = await _context.Students.ToListAsync();

        //    var gotStudent = allStudents.FirstOrDefault(s => s.StudentID == request.StdId);

        //    return gotStudent;
        //}
        public async Task<ShowStudentDTO> Handle(GetStudentByID request, CancellationToken cancellationToken)
        {
            var allStudents = await _context.Students.ToListAsync();

            var gotStudent = allStudents.FirstOrDefault(s => s.StudentID == request.StdId);

            if (gotStudent == null)
            {
                return null;
            }


            //var showStudentDTO = new ShowStudentDTO
            //{
            //    StudentID = gotStudent.StudentID,
            //    StudentName = gotStudent.StudentName,
            //    StudentAge = gotStudent.StudentAge.ToString(), 
            //    StudentEmail = gotStudent.StudentEmail,
            //    CreatedDetails = $"{ gotStudent.CreatedBy} - { gotStudent.CreatedDate} ",
            //    LastModifiedDate = gotStudent.LastModifiedDate,
            //    LastModifiedBy = gotStudent.LastModifiedBy,
            //    IsActive = gotStudent.IsActive
            //};



            var showStudentDTO = _mapper.Map<ShowStudentDTO>(gotStudent);


            return showStudentDTO;


        }
    }
}
