using System;
using System.Collections.Generic;
using System.Text;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Dtos
{
    public class InternalFriendshipDto : IDto
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationUser ApplicationUserTwo { get; set; }
    }
}
