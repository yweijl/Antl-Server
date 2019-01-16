namespace Antl.WebServer.Entities
{
    public class Friendship : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string ApplicationUserExternalId {get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserTwoExternalId { get; set; }
        public ApplicationUser ApplicationUserTwo { get; set; }
    }
}