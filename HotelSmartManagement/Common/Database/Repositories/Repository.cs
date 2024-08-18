using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.Common.Database.Misc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IDatabaseObject
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(HotelDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async void Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async void AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async void AddRange(params T[] entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public abstract IQueryable<T> AsQueryable();

        public async Task<bool> Contains(T entity)
        {
            return await _dbSet.ContainsAsync(entity);
        }

        public async Task<bool> ContainsAny(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate) != null;
        }

        public async Task<bool> ContainsById(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId) != null;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public int DeleteBy(Expression<Func<T, bool>> predicate)
        {
            var matchingEntities = _dbSet.Where(predicate);
            _dbSet.RemoveRange(matchingEntities);
            return matchingEntities.Count();
        }

        public async void DeleteById(Guid id)
        {
            var entityWithId = await _dbSet.FindAsync(id);
            if (entityWithId != null)
            {
                _dbSet.Remove(entityWithId);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void DeleteRange(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return AsQueryable().AsEnumerable();
        }

        public Task<T?> GetBy(Expression<Func<T, bool>> predicate)
        {
            return AsQueryable().FirstOrDefaultAsync(predicate);
        }

        public Task<T?> GetById(Guid id)
        {
            return AsQueryable().FirstOrDefaultAsync(entity => entity.UniqueId == id);
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
