using System;
using System.Collections.Generic;
using System.Text;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Dtos
{
    public class InternalFriendshipProjection : FriendshipDto
    {
        public ApplicationUser LeftApplicationUser { get; set; }
        public ApplicationUser RightApplicationUser { get; set; }
    }
}
