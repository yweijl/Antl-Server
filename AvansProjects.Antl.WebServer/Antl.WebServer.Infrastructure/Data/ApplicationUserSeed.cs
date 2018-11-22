using System.Collections.Generic;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Infrastructure.Data
{
    public class ApplicationUserSeed
    {
        public IList<ApplicationUser> SeedUsers()
        {
            var userList = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "user1",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "jdoe@gmail.com",
                    Gender = GenderType.Male,
                },
                new ApplicationUser
                {
                    UserName = "user2",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@gmail.com",
                    Gender = GenderType.Female,
                }
            };

            userList[0].FriendShips = new List<FriendShip>
            {
                new FriendShip
                {
                    ApplicationUser = userList[1]
                }
            };

            userList[1].FriendShips = new List<FriendShip>
            {
                new FriendShip
                {
                    ApplicationUser = userList[0]
                }
            };
            return userList;
        }
    }
}