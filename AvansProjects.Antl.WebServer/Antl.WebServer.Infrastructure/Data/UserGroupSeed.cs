using System.Collections.Generic;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Infrastructure.Data
{
    public class UserGroupSeed
    {
        public Group SeedGroups(IList<ApplicationUser> applicationUserList)
            =>
                new Group
                {
                    Name = "Groepje",
                    UserGroups = new List<UserGroup>
                    {
                        new UserGroup
                        {
                            ApplicationUserId = applicationUserList[0].Id,
                            ApplicationUser = applicationUserList[0],
                        },
                        new UserGroup
                        {
                            ApplicationUserId = applicationUserList[1].Id,
                            ApplicationUser = applicationUserList[1]
                        }
                    }
                };
    };
}
