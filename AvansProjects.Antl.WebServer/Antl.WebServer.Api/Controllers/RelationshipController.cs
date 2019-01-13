using System.Collections.Generic;
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



        [
            HttpGet("user/{code}")
        ]

        public async Task<IActionResult> GetAllAsync(string code)

        {
            var result = await ((ContactService) _service).GetAllFriendsAsync(code).ConfigureAwait(true);
            var mappedResult = new List<FriendDto>();
            foreach (var applicationUser in result)
            {
            mappedResult.Add(Mapper.Map(applicationUser).ToANew<FriendDto>());
                
            }
            return Ok(mappedResult);
        }
    }
}