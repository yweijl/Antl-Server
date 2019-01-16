using System.Collections.Generic;

namespace Antl.WebServer.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}