using EmployeeManagementSystem.Common.GenericRepository;
using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Repository
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeManagementSystem.Data.Entities.Employee>
    {
       
    }
}
