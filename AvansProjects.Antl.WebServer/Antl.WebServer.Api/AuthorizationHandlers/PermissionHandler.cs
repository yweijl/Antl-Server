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
                                            x.Issuer == "http://10.0.2.2:64151"))
            //if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role &&
            //                                x.Issuer == "https://antlwebserver.azurewebsites.net"))
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(userRole.Role))
                context.Succeed(userRole);

            return Task.CompletedTask;
        }
    }
}