using System;

namespace Antl.WebServer.Dtos
{
    public class EventDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? MainDateTime { get; set; }
        public string Location { get; set; }
    }
}