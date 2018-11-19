using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}