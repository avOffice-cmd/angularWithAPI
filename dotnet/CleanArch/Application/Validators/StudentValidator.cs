using Application.DTOs.StudentDTOs;
using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class StudentValidator : AbstractValidator<AddStudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(s => s.StudentAge)
                .Must(age => int.TryParse(age, out var ageValue) && ageValue > 0)
                .WithMessage("Student age must be a valid integer greater than 0");
        }

    }

}
