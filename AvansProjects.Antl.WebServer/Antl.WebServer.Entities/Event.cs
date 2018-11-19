using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime MainDateTime { get; set; }
        public string Location { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public ICollection<DateTime> DateTimes { get; set; }
    }
}