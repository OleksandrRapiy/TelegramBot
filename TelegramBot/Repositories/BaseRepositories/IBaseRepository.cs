using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramBotApp.Data;

namespace TelegramBotApp.Repositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        DbSet<T> Collections { get; }
        Task<T> AddAsync(T entity);
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> predicator);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicator, params Expression<Func<T, object>>[] expression);

        Task<IEnumerable<T>> GetAllAsync();

    }
}
