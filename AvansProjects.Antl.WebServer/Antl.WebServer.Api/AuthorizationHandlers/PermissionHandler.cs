using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Antl.WebServer.Api.AuthorizationHandlers
{
    public class PermissionHandler : AuthorizationHandler<UserRole>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRole userRole)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role &&
                                            x.Issuer == "http://localhost:44362/api"))
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(userRole.Role))
                context.Succeed(userRole);

            return Task.CompletedTask;
        }
    }
}