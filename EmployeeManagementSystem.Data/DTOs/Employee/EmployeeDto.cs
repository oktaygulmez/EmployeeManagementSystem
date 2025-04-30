using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Adress { get; set; }

        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}
