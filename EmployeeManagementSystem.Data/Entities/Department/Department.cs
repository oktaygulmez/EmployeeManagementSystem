using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Entities
{
    public class Department : BaseEntity
    {
        public Guid Id { get; set; }
        public string DepatmentName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
