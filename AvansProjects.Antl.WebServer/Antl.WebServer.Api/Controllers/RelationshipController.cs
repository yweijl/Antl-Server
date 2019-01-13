using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
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
    }
}