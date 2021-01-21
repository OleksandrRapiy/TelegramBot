using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestIBWT.Data;

namespace TestIBWT.Repositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        DbSet<T> Collections { get; }
        Task<T> AddAsync(T entity);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> predicator);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<IEnumerable<T>> GetAllAsync();

    }
}
