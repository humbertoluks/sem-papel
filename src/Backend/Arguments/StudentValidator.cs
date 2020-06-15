using Domain.Dtos;
using FluentValidation;

namespace Backend.Arguments
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
             RuleFor(student => student.Id).NotNull();
             RuleFor(student => student.Document.Number).NotNull().NotEmpty().Length(11,11);
             RuleFor(student => student.Email.Address).NotNull().MinimumLength(3).MaximumLength(50);
             RuleFor(student => student.Name.Firstname).NotNull().MinimumLength(3).MaximumLength(50);
             RuleFor(student => student.Name.Lastname).NotNull().MinimumLength(3).MaximumLength(50);
        }
    }
} 