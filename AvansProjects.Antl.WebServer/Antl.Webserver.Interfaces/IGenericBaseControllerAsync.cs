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
        Task<IActionResult> GetAsync(int id);
        Task<IActionResult> PostAsync([FromBody] TDto tDto);
        Task<IActionResult> PutAsync([FromBody] TDto tDto);
        Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<TDto> patchDoc);
        Task<IActionResult> DeleteAsync(int id);
    }
}
