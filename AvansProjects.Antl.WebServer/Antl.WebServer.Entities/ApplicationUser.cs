using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Antl.WebServer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ApplicationUser> FriendList { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Event> Events { get; set; }
        public GenderType Gender { get; set; }
    }
}