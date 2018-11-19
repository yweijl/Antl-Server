namespace Antl.WebServer.Entities
{
    public class UserGroup
    {
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}