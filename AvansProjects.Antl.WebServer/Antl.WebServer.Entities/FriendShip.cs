﻿using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Entities
{
    public class FriendShip : IEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}