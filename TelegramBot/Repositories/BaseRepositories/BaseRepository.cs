using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramBotApp.Data;

namespace TelegramBotApp.Repositories.BaseRepositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly TelegramBotContext _context;
        public DbSet<T> Collections { get; }
        protected BaseRepository(TelegramBotContext context)
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

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicator, params Expression<Func<T, object>>[] expression)
        {
            var query = Collections;

            foreach (var item in expression)
                query.Include(item);

            return await Collections.Where(predicator).ToListAsync();
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
