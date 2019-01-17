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
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByExternalIdAsync(string externalId);
        Task<bool> UpdateAsync(TDto tDto);
        Task<bool> PatchAsync(string externalId, Func<TDto, TDto> patchFunction);
        Task<bool> DeleteAsync(string externalId);
    }
}