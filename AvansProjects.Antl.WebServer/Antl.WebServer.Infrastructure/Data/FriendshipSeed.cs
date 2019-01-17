using System.Collections.Generic;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Infrastructure.Data
{
    public class FriendshipSeed
    {
        public Friendship SeedFriendShips(ApplicationUser user, ApplicationUser userTwo)
        {
            var friendShip = new Friendship
            {
                    ApplicationUser = user,
                    ApplicationUserFriend = userTwo
            };

            return friendShip;
        }
    }
}