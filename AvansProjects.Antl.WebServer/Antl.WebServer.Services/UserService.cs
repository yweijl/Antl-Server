using System;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Services
{
    public class UserService : GenericServiceAsync<UserDto, ApplicationUser>
    {
        public UserService(IGenericRepository<ApplicationUser> repository) : base(repository)
        {
        }

        public override async Task<ApplicationUser> GetAsync(int id)
        {
            return await base.GetAsync(id);
        }
    }
}