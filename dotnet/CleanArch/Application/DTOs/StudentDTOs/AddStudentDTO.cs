using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.StudentDTOs
{
    public class AddStudentDTO
    {
        public string StudentFullName { get; set; }
        public string StudentAge { get; set; }
        public string StudentEmail { get; set; }

        public string StudentAddress { get; set; }
        public string StudentCityOrCountry { get; set;}
    }
}
