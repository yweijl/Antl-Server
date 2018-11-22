using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class EventDate : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Event Event { get; set; }
        public ICollection<UserEventAvailability> UserAvailabilities { get; set; }
    }
}