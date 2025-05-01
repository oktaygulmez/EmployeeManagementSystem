using EmployeeManagementSystem.MediatR.Department.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Validators
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator() 
        {
            RuleFor(c => c.DepartmentName).NotEmpty().WithMessage("Please enter departman name.");
        }
    }
}
