using System;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Dtos
{
    public class EventDateDto
    {
        public DateTime DateTime { get; set; }
        public int EventId { get; set; }
    }
}