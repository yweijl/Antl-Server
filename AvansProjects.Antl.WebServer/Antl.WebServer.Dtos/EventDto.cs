using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Antl.WebServer.Dtos
{
    public class EventDto : IDto
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
        public bool IsOwner { get; set; }
        public ICollection<EventDateDto> EventDates { get; set; }
    }
}