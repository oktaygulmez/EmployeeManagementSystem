using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Common.UnitOfWork;
using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Common.GenericRepository
{
    public class GenericRepository<TC, TContext> : IGenericRepository<TC>
        where TC : class 
        where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<TC> _dbSet;
        protected IUnitOfWork<TContext> _uow;
        public GenericRepository(IUnitOfWork<TContext> uow)
        {
            _context = uow.Context;
            this._uow = uow;
            _dbSet = _context.Set<TC>();
        }

        public IQueryable<TC> All => _context.Set<TC>();

        public async Task<IEnumerable<TC>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TC> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual void Add(TC entity)
        {
             _context.AddAsync(entity);
        }

        public virtual void Update(TC entity)
        {
            _context.Update(entity);
        }

        public virtual void Delete(TC entityData)
        {
            var entity = entityData as BaseEntity;
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Update(entity);
            }
        }

    }
}