using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Dtos
{
    public class InternalEventDto : EventDto
    {
        public int? EventOwnerId { get; set; }
        public ApplicationUser EventOwner { get; set; }
    }
}