using System;
using System.Collections.Generic;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Entities
{
    public class SendEventDto : IDto
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
        public bool IsOwner { get; set; }
        public bool? IsDeleted { get; set; }
        public int? EventOwnerId { get; set; }
        public int  Hash { get; set; }
        public ICollection<EventDateDto> EventDates { get; set; }
    }
}
