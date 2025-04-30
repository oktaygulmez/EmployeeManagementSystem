using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Command
{
    public class CreateDepartmentCommand : IRequest<ServiceResponse<DepartmentDto>>
    {
        public string DepartmentName { get; set; }
    }
}
