using System.Linq.Expressions;
using HotelSmartManagement.Common.Database.Misc;

namespace HotelSmartManagement.Common.Database.Repositories
{
    public interface IRepository<T> where T : class, IDatabaseObject
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        T? GetBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void AddRange(params T[] entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(Guid id);
        int DeleteBy(Expression<Func<T, bool>> predicate);
        void DeleteRange(IEnumerable<T> entities);
        void DeleteRange(params T[] entities);
        bool Contains(T entity);
        bool ContainsById(Guid entityId);
        bool ContainsAny(Expression<Func<T, bool>> predicate);
        void Save();
        IQueryable<T> AsQueryable();
    }
}
