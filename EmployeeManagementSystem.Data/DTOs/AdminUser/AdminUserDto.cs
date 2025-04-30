using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.DTOs.AdminUser
{
    public class AdminUserDto
    {
        public string Id { get; set; }

        public string EMail { get; set; }

        public string HashedPassword { get; set; }
    }
}
