using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Antl.WebServer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Antl.WebServer.Infrastructure.Data
{
    public static class IdentitySeed
    {
        public static async Task SetIdentityRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager, IList<ApplicationUser> applicationUserList)
        {
            await roleManager.CreateAsync(new IdentityRole<int>("User")).ConfigureAwait(false);
            await roleManager.CreateAsync(new IdentityRole<int>("Admin")).ConfigureAwait(false);

            var user1 = applicationUserList[0];
            await userManager.CreateAsync(user1, "Querty1!").ConfigureAwait(false);
            var emailToken1 = await userManager.GenerateEmailConfirmationTokenAsync(user1).ConfigureAwait(false);
            await userManager.ConfirmEmailAsync(user1, emailToken1).ConfigureAwait(false);
            await userManager.AddToRoleAsync(user1, "Admin").ConfigureAwait(false);

            var user2 = applicationUserList[1];
            await userManager.CreateAsync(user2, "Querty1!").ConfigureAwait(false);
            var emailToken2 = await userManager.GenerateEmailConfirmationTokenAsync(user2).ConfigureAwait(false);
            await userManager.ConfirmEmailAsync(user2, emailToken2).ConfigureAwait(false);
            await userManager.AddToRoleAsync(user2, "User").ConfigureAwait(false);
        }
    }
}