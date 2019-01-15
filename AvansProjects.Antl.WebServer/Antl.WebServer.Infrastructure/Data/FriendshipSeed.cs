using System.Collections.Generic;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Infrastructure.Data
{
    public class FriendshipSeed
    {
        public FriendShip SeedFriendShips(ApplicationUser user, ApplicationUser userTwo)
        {
            var friendShip = new FriendShip
            {
                    ApplicationUser = user,
                    ApplicationUserTwo = userTwo
            };

            return friendShip;
        }
    }
}