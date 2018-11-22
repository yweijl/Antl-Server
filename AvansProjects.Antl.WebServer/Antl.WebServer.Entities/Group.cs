using System;
using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
    }
}