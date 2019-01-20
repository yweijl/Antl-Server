using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : GenericBaseControllerAsync<EventDto, Event>
    {
        private readonly IEventService _eventService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(IGenericServiceAsync<EventDto, Event> genericService, IEventService eventService , UserManager<ApplicationUser> userManager) : base(genericService)
        {
            _eventService = eventService;
            _userManager = userManager;
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync(EventDto eventDto)
        {
            int.TryParse(_userManager.GetUserId(User), out var userId);
            var result = await _eventService.AddAsync(eventDto, userId).ConfigureAwait(true);

            return result == null
                ? (IActionResult)NotFound($"Could not add {typeof(EventDto).Name}")
                : Ok(result);
        }

        [HttpGet("sync")]
        public async Task<IActionResult> SyncAsync()
        {
            int.TryParse(_userManager.GetUserId(User), out var userId);
            var result = await _eventService.GetHashList(userId).ConfigureAwait(false);

            return result == null
                ? (IActionResult)NotFound($"Could not add {typeof(EventDto).Name}")
                : Ok(result);
        }
    }
}