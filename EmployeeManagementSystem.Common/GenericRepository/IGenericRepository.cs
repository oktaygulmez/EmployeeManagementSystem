using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Common.GenericRepository
{
    public interface IGenericRepository<TC> where TC : class
    {
        Task<IEnumerable<TC>> GetAllAsync();
        Task<TC> GetByIdAsync(Guid id);
        Task AddAsync(TC entity);
        Task UpdateAsync(TC entity);
        Task DeleteAsync(Guid id);
    }
}
