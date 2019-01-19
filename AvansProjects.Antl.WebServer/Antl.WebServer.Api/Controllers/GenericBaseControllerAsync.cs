using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Identity;

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

        [HttpGet("{externalId}")]
        public virtual async Task<IActionResult> GetAsync(string externalId)
        {
            var entity = (await _service.GetByExternalIdAsync(externalId).ConfigureAwait(true));
            if (entity == null) return NotFound($"{typeof(TEntity).Name} with id: {externalId} was not found");

            var dto = Mapper.Map(entity).ToANew(typeof(TDto));
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody] TDto dto)
        {
            if (dto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddAsync(dto).ConfigureAwait(true);
            return string.IsNullOrEmpty(result)
                ? (IActionResult)NotFound($"Could not add {typeof(TDto).Name}")
                : Ok(result);
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
        public virtual async Task<IActionResult> PatchAsync(string externalId, [FromBody] JsonPatchDocument<TDto> patchDoc)
        {
            if (patchDoc == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            return await _service.PatchAsync(externalId, entity =>
            {
                patchDoc.ApplyTo(entity, ModelState);
                return entity;
            }).ConfigureAwait(true)
                ? NoContent()
                : (IActionResult)NotFound($"Could not make changes to {typeof(TDto).Name}");
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(string externalId)
        {
            var x = await _service.DeleteAsync(externalId).ConfigureAwait(true);
            return NoContent();
        }
    }
}