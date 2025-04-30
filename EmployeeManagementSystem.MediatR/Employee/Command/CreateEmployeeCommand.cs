using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Helper;

namespace EmployeeManagementSystem.MediatR.Employee.Command
{
    public class CreateEmployeeCommand : IRequest<ServiceResponse<EmployeeDto>>
    {
        public string Name { get; set; }

        public string SurName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
