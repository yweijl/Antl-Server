using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antl.WebServer.Services
{
    public class GenericServiceAsync<TDto, TEntity> : IGenericServiceAsync<TDto, TEntity>
        where TDto : class, IDto
        where TEntity : IEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericServiceAsync(IGenericRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<TEntity> AddAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = Mapper.Map(dto).ToANew<TEntity>();
            entity.ExternalId = Generate.ExternalId();
            return await _repository.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async  Task<bool> DeleteAsync(string externalId)
        {
            var entity = await _repository.GetAsync(x => x.ExternalId == externalId).ConfigureAwait(false);
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return await _repository.DeleteAsync(entity).ConfigureAwait(false);
        }

        public virtual Task<TEntity> GetByIdAsync(int id)
        {
            return _repository.GetAsync(x => x.Id == id);
        }

       public virtual Task<TEntity> GetByExternalIdAsync(string externalId)
        {
            return _repository.GetAsync(x => x.ExternalId == externalId);
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync() =>
            await _repository.GetAllAsync().ConfigureAwait(false);

        public virtual async Task<bool> PatchAsync(string externalId, Func<TDto, TDto> patchFunction)
        {
            if (patchFunction == null) throw new ArgumentNullException(nameof(patchFunction));
            var entity = await GetByExternalIdAsync(externalId).ConfigureAwait(false);

            return entity != null &&
                   await UpdateAsync(patchFunction(Mapper.Map(entity)
                       .ToANew<TDto>())).ConfigureAwait(false);
        }

        public virtual async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = await _repository.GetAsync(x => x.ExternalId == dto.ExternalId).ConfigureAwait(false);

            if (entity == null) throw new ArgumentNullException(nameof(dto));

            var patchedEntity = Mapper.Map(dto).Over(entity);

            return await _repository.UpdateAsync(patchedEntity).ConfigureAwait(false);
        }
    }
}