using Antl.WebServer.Entities;
using Antl.WebServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Threading.Tasks;

namespace Antl.WebServer.Infrastructure
{
    public static class AntlSeed
    {
        public static async Task SeedAsync(AntlContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!context.ApplicationUsers.Any()
                && !context.Groups.Any()
                && !context.Events.Any())
                await StartSeedAsync(context, userManager, roleManager).ConfigureAwait(false);
                Console.WriteLine("Seed Done");
        }

        private static async Task StartSeedAsync(AntlContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var applicationUserList = new ApplicationUserSeed().SeedUsers();

            if (applicationUserList == null)
                throw new ArgumentNullException();

            await IdentitySeed.SetIdentityRoleAsync(userManager, roleManager, applicationUserList).ConfigureAwait(false);

            var events = new EventSeed().SeedEvents(applicationUserList);

            if (events == null)
                throw new ArgumentNullException();

            foreach (var appointment in events)
            {
                await context.Events.AddAsync(appointment).ConfigureAwait(false);
            }

            var group = new UserGroupSeed().SeedGroups(applicationUserList);

            await context.Groups.AddAsync(group).ConfigureAwait(false);

            await context.SaveChangesAsync().ConfigureAwait(false);

        }
    }
}
