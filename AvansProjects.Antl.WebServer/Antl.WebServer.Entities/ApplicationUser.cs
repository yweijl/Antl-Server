﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Antl.WebServer.Entities
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ExternIdGuid { get; set; }
        public ICollection<FriendShip> FriendShips { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<UserEventAvailability> UserEventAvailabilities { get; set; }
        public GenderType Gender { get; set; }
    }
}