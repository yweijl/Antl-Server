namespace Antl.WebServer.Dtos
{
    public class EventSyncDto : IDto
    {
        public string ExternalId { get; set; }
        public int Hash { get; set; }
    }
}