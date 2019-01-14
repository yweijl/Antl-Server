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
                    ExternalId = "0000-0000-0000",
                    Gender = GenderType.Male,
                },
                new ApplicationUser
                {
                    UserName = "user2",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "janedoe@gmail.com",
                    ExternalId = "9999-9999-9999",
                    Gender = GenderType.Female,
                }
            };

            return userList;
        }
    }
}