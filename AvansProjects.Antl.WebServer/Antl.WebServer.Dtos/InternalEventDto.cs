using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Antl.WebServer.Dtos
{
    public class InternalEventDto : EventDto
    {
        public int EventOwnerId { get; set; }
    }
}