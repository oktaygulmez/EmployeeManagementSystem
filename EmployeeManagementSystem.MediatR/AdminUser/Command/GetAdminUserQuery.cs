using EmployeeManagementSystem.Data.DTOs.AdminUser;
using MediatR;

namespace EmployeeManagementSystem.MediatR.AdminUser.Command
{
    public class GetAdminUserQuery : IRequest<AdminUserDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
