using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Antl.WebServer.Interfaces
{
    public interface IGenericBaseControllerAsync<TDto>
    where TDto : class, IDto
    {
        Task<IActionResult> GetAllAsync();
        Task<IActionResult> GetAsync(string externalId);
        Task<IActionResult> PostAsync([FromBody] TDto tDto);
        Task<IActionResult> PutAsync([FromBody] TDto tDto);
        Task<IActionResult> PatchAsync(string externalId, [FromBody] JsonPatchDocument<TDto> patchDoc);
        Task<IActionResult> DeleteAsync(string externalId);
    }
}
