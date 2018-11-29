using System.Collections.Generic;
using System.Threading.Tasks;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}