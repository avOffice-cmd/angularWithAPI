using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.StudentDTOs
{
    public class UpdateStudentDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentAge { get; set; }
        public string StudentEmail { get; set; }
        public string StudentAddress { get; set; }
        public string AgeGroupCount { get; set; }
        
        public string StudentCity { get; set; }
        public string StudentCountry { get; set; }
    }
}
