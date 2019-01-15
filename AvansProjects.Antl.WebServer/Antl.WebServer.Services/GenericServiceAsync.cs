using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Services{
    public class GenericServiceAsync<TDto, TEntity> : IGenericServiceAsync<TDto, TEntity>
        where TDto : class, IDto
        where TEntity : IEntity
    {
        protected readonly IGenericRepository<TEntity> _repository;

        public GenericServiceAsync(IGenericRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual async Task<TEntity> AddAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = Mapper.Map(dto).ToANew<TEntity>();

            var result = await _repository.AddAsync(entity).ConfigureAwait(false);
            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetAsync(id).ConfigureAwait(false);
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return await _repository.DeleteAsync(entity).ConfigureAwait(false);
        }

        public virtual Task<TEntity> GetAsync(int id)
        {
            return _repository.GetAsync(id);
        }

        public async Task<ICollection<TEntity>> GetAllAsync() =>
            await _repository.GetAllAsync().ConfigureAwait(false);

        public async Task<bool> PatchAsync(int id, Func<TDto, TDto> patchFunction)
        {
            if (patchFunction == null) throw new ArgumentNullException(nameof(patchFunction));
            var entity = await GetAsync(id).ConfigureAwait(false);

            return entity != null &&
                   await UpdateAsync(patchFunction(Mapper.Map(entity)
                       .ToANew<TDto>())).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            var entity = await _repository.GetAsync(dto.Id).ConfigureAwait(false);

            if (entity == null) throw new ArgumentNullException(nameof(dto));

            var patchedEntity = Mapper.Map(dto).Over(entity);

            return await _repository.UpdateAsync(patchedEntity).ConfigureAwait(false);
        }
    }
}