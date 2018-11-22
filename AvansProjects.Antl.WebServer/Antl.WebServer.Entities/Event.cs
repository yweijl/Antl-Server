using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Event : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
        public ICollection<EventDate> EventDates { get; set; }
    }
}