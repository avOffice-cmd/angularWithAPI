using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.StudentDTOs
{
    public class ShowStudentDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentAge { get; set; }
        public string StudentEmail { get; set; }
        public string? CreatedDetails { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
