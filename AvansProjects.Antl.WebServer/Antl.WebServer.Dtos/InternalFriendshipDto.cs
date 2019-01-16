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
        public string ExternalId { get; set; }
        public int ApplicationUserId { get; set; }
        public int ApplicationUserTwoId { get; set; }
    }
}
