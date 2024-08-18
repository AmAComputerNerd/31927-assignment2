using HotelSmartManagement.Common.Database.Context;
using HotelSmartManagement.Common.Database.Misc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, IDatabaseObject
    {
        protected readonly DbContext _context;
        protected DbSet<T> _dbSet;

        public Repository(HotelDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void AddRange(params T[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public abstract IQueryable<T> AsQueryable();

        public bool Contains(T entity)
        {
            return _dbSet.Contains(entity);
        }

        public bool ContainsAny(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate) != null;
        }

        public bool ContainsById(Guid entityId)
        {
            return _dbSet.Find(entityId) != null;
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

        public void DeleteById(Guid id)
        {
            var entityWithId = _dbSet.Find(id);
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

        public T? GetBy(Expression<Func<T, bool>> predicate)
        {
            return AsQueryable().FirstOrDefault(predicate);
        }

        public T? GetById(Guid id)
        {
            return AsQueryable().FirstOrDefault(entity => entity.UniqueId == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
