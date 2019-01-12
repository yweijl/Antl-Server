using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;

namespace Antl.WebServer.Api.Controllers
{
    [ApiController]
    public class GenericBaseControllerAsync<TDto, TEntity> : ControllerBase, IGenericBaseControllerAsync<TDto>
        where TDto : class, IDto
        where TEntity : IEntity
    {
        private readonly IGenericServiceAsync<TDto, TEntity> _service;

        public GenericBaseControllerAsync(IGenericServiceAsync<TDto, TEntity> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync() =>
            Ok(await _service.GetAllAsync().ConfigureAwait(true));

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(int id)
        {
            var entity = (await _service.GetAsync(id).ConfigureAwait(true));
            if (entity == null) return NotFound($"{typeof(TEntity).Name} with id: {id} was not found");

            var dto = Mapper.Map(entity).ToANew(typeof(TDto));
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody] TDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _service.AddAsync(dto).ConfigureAwait(true);
            return entity == null
                ? (IActionResult)NotFound($"Could not add {typeof(TDto).Name}")
                : Ok(entity);
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync([FromBody] TDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.UpdateAsync(dto).ConfigureAwait(true)
                ? NoContent()
                : (IActionResult)NotFound($"Could not make changes to {typeof(TDto).Name}");
        }

        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<TDto> patchDoc)
        {
            if (patchDoc == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.PatchAsync(id, entity =>
            {
                patchDoc.ApplyTo(entity, ModelState);
                return entity;
            }).ConfigureAwait(true)
                ? NoContent()
                : (IActionResult)NotFound($"Could not make changes to {typeof(TDto).Name}");
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int id)
        {
            var x = await _service.DeleteAsync(id).ConfigureAwait(true);
            return NoContent();
        }
    }
}