using System;
using System.Collections.Generic;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Dtos
{
    public class EventDto : IDto
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
        public ICollection<EventDate> EventDates { get; set; }
    }
}