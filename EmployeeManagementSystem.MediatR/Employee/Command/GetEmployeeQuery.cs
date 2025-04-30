using EmployeeManagementSystem.Data.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Employee.Command
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
    }
}
