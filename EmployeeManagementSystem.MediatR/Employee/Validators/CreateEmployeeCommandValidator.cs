using EmployeeManagementSystem.MediatR.Employee.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Employee.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Please enter the name.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(c => c.SurName)
                .NotEmpty().WithMessage("Please enter the surname.")
                .MaximumLength(50).WithMessage("Surname cannot be longer than 50 characters.");

            RuleFor(c => c.EMail)
                .NotEmpty().WithMessage("Please enter the email.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Please enter the phone number.")
                .Matches(@"^[0-9]{10,15}$").WithMessage("Phone number must be 10 to 15 digits.")
                .MaximumLength(15).WithMessage("Phone number cannot be longer than 15 characters.");

            RuleFor(c => c.Adress)
                .NotEmpty().WithMessage("Please enter the address.")
                .MaximumLength(250).WithMessage("Address cannot be longer than 250 characters.");

            RuleFor(c => c.DepartmentId)
                .NotEmpty().WithMessage("Please select a department.");
        }
    }
}