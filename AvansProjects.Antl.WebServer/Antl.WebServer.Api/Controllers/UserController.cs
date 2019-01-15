using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Antl.WebServer.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : GenericBaseControllerAsync<UserDto, ApplicationUser>
    {
        public UserController(IGenericServiceAsync<UserDto, ApplicationUser> service) : base(service)
        {
        }
    }
}