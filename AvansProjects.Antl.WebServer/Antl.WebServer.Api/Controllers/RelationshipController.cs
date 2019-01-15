using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Antl.WebServer.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class RelationshipController : GenericBaseControllerAsync<FriendshipDto, FriendShip>
    {
        public RelationshipController(IGenericServiceAsync<FriendshipDto, FriendShip> service) : base(service)
        {
        }

        [HttpGet("user/{code}")]
        public async Task<IActionResult> GetAllAsync(string code)

        {
            var result = await ((ContactService)_service).GetAllFriendsAsync(code).ConfigureAwait(true);

            var mappedResult = result.Select(x => Mapper.Map(x).ToANew<FriendDto>()).ToList();

            return Ok(mappedResult);
        }
    }
}