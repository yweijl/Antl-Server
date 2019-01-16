using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class EventDate : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime DateTime { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public ICollection<UserEventDate> UserAvailabilities { get; set; }
    }
}