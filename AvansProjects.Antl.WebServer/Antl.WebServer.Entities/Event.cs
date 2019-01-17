using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Event : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
        public virtual ICollection<EventDate> EventDates { get; set; }
    }
}