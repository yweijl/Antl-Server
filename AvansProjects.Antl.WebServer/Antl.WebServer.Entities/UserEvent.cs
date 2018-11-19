using System;

namespace Antl.WebServer.Entities
{
    public class UserEvent
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public Availability Availability { get; set; }
    }
}