using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Data.DTOs;
using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       Task<List<EmployeeDto>> GetEmployeeAll();
       Task<EmployeeDto> GetEmployeeById(Guid id);

    }
}
