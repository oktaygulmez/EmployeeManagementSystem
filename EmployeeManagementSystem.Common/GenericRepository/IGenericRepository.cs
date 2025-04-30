using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Common.GenericRepository
{
    public interface IGenericRepository<TC>
        where TC : class
    {
        IQueryable<TC> All { get; }
        Task<IEnumerable<TC>> GetAllAsync();
        Task<TC> GetByIdAsync(Guid id);
        void Add(TC entity);
        void Update(TC entity);
        void Delete(TC entity);
    }
}
