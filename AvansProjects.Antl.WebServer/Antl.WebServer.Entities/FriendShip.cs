namespace Antl.WebServer.Entities
{
    public class FriendShip : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationUser ApplicationUserTwo { get; set; }
    }
}