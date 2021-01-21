using BotIBWT;
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
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly BotIBWTContext _context;
        public DbSet<T> Collections { get; }
        protected BaseRepository(BotIBWTContext context)
        {
            _context = context;
            Collections = _context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = Collections.Add(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Collections.AsNoTracking();

            if(include != null)
                query = include(query);

            query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> predicator)
        {
            return await Collections.Where(predicator).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collections.ToListAsync();
        }
    }
}
