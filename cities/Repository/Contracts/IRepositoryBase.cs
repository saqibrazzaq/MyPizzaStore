using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace cities.Repository.Contracts
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> FindAll(bool trackChanges);
        IEnumerable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges,
            Func<IEnumerable<T>, IIncludableQueryable<T, object>> include = null
            );
        void Create(T entity);
        void Delete(T entity);
    }
}
