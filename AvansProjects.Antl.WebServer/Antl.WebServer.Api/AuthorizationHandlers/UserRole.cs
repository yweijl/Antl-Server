using Microsoft.AspNetCore.Authorization;

namespace Antl.WebServer.Api.AuthorizationHandlers
{
    public class UserRole : IAuthorizationRequirement
    {
        public string Role { get; }

        public UserRole(string role)
        {
            Role = role;
        }
    }
}