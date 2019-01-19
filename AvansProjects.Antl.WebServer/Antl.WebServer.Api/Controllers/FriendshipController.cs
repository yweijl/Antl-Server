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
    public class FriendshipController : GenericBaseControllerAsync<FriendshipDto, Friendship>
    {
        private readonly IFriendshipService _friendshipService;
        private readonly UserManager<ApplicationUser> _userManager;


        public FriendshipController(IGenericServiceAsync<FriendshipDto, Friendship> genericService , IFriendshipService friendshipService, UserManager<ApplicationUser> userManager) : base(genericService)
        {
            _friendshipService = friendshipService;
            _userManager = userManager;
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync([FromBody] FriendshipDto friendshipDto)
        {
            int.TryParse(_userManager.GetUserId(User), out var userId);

            if (friendshipDto == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _friendshipService.AddAsync(friendshipDto, userId).ConfigureAwait(true);

            return string.IsNullOrEmpty(result)
                    ? (IActionResult) UnprocessableEntity()
                    : Ok("Friendship added");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetListAsync()
        {
            int.TryParse(_userManager.GetUserId(User), out var userId);

            var result = await _friendshipService.GetListAsync(userId).ConfigureAwait(true);

            return result == null
                ? (IActionResult)NotFound($"Could not retrieve list {typeof(FriendDto).Name}")
                : Ok(result);
        }
    }
}