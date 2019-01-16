using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Antl.WebServer.Services;
using Microsoft.AspNetCore.Identity;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class FriendshipController : GenericBaseControllerAsync<FriendshipDto, FriendShip>
    {
        private readonly IFriendshipService _friendshipService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FriendshipController(IGenericServiceAsync<FriendshipDto, FriendShip> genericService , IFriendshipService friendshipService, UserManager<ApplicationUser> userManager) : base(genericService)
        {
            _friendshipService = friendshipService;
            _userManager = userManager;
        }

        [HttpGet("get/{externalId}")]
        public async Task<IActionResult> GetListAsync(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                return BadRequest($"Invalid request with id: {externalId}");
                   
            var userId = int.Parse(_userManager.GetUserId(User));
            var result = await _friendshipService.GetListAsync(externalId, userId).ConfigureAwait(true);

            var mappedResult = result.Select(x => Mapper.Map(x).ToANew<FriendDto>()).ToList();

            return Ok(mappedResult);
        }
    }
}