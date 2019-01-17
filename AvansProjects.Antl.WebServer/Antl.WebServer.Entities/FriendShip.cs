namespace Antl.WebServer.Entities
{
    public class Friendship : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int ApplicationUserId {get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserFriendId { get; set; }
        public ApplicationUser ApplicationUserFriend { get; set; }
    }
}