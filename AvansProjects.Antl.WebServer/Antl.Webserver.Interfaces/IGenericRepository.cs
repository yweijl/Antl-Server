using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> where);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}