using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Interfaces
{
    public interface IGenericServiceAsync<TDto, TEntity>
        where TDto : class, IDto
        where TEntity : IEntity
    {
        Task<TEntity> AddAsync(TDto tDto);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(int id);
        Task<bool> UpdateAsync(TDto tDto);
        Task<bool> PatchAsync(int id, Func<TDto, TDto> patchFunction);
        Task<bool> DeleteAsync(int id);
    }
}