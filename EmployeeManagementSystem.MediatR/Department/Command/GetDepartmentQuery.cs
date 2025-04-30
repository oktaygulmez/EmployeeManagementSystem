using EmployeeManagementSystem.Data.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Command
{
    public class GetDepartmentQuery : IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
    }
}
