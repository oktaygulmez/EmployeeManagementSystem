using EmployeeManagementSystem.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Command
{
    public class DeleteDepartmentCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}

