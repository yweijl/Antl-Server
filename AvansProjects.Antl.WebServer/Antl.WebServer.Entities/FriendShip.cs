namespace Antl.WebServer.Entities
{
    public class Friendship : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int LeftApplicationUserId {get; set; }
        public ApplicationUser LeftApplicationUser { get; set; }
        public int RightApplicationUserId { get; set; }
        public ApplicationUser RightApplicationUser { get; set; }
    }
}