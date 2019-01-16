namespace Antl.WebServer.Entities
{
    public class UserEventDate : IEntity
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int EventDateID { get; set; }
        public EventDate EventDate { get; set; }
        public Availability Availability { get; set; }
    }
}