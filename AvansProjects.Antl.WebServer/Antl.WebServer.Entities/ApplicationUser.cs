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
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Friendship> Friendships { get; set; }
        public virtual ICollection<Friendship> USerFriendships  { get; set; }
    }
}