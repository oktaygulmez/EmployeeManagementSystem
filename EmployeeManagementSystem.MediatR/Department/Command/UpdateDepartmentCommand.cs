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
    public class UpdateDepartmentCommand : IRequest<ServiceResponse<DepartmentDto>>
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
    }
}
