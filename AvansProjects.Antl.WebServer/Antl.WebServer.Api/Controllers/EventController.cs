using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : GenericBaseControllerAsync<EventDto, Event>
    {
        public EventController(IGenericServiceAsync<EventDto, Event> service) : base(service)
        {
        }

        [HttpPost]
        public override Task<IActionResult> PostAsync(EventDto dto)
        {
            return base.PostAsync(dto);
        }
    }
}