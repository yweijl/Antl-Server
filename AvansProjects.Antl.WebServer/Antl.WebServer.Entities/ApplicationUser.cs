using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExternalId { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserEventDate> UserEventAvailabilities { get; set; }
        public virtual ICollection<Friendship> LeftFriendships { get; set; }
        public virtual ICollection<Friendship> RightFriendships  { get; set; }
    }
}