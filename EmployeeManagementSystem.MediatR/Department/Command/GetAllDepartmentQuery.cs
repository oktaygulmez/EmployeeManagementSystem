using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Department.Command
{
    public class GetAllDepartmentQuery : IRequest<List<DepartmentDto>>
    {
    }
}
