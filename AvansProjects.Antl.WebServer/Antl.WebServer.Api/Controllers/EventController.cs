using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class EventController : GenericBaseControllerAsync<EventDto, Event>
    {
        public EventController(IGenericServiceAsync<EventDto, Event> service) : base(service)
        {
        }
    }
}