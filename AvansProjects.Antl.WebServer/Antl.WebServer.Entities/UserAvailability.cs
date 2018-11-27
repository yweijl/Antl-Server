using Antl.WebServer.Interfaces;

namespace Antl.WebServer.Entities
{
    public class UserEventAvailability : IEntity
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public EventDate Event { get; set; }
        public Availability Availability { get; set; }
    }
}