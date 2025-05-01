using EmployeeManagementSystem.MediatR.Department.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Validators
{
    internal class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentCommandValidator()
        {
            RuleFor(c => c.DepartmentName).NotEmpty().WithMessage("Please enter departman name.");
        }
    }
}

