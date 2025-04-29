using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.Employee.Command
{
    public class UpdateEmployeeCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public Guid DepartmentId { get; set; }
    }
}
