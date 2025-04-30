using EmployeeManagementSystem.Data.DTOs.AdminUser;
using EmployeeManagementSystem.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.MediatR.AdminUser.Command
{
    public class GetAdminUserQuery : IRequest<AdminUserDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
