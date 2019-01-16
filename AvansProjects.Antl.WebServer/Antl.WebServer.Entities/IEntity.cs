namespace Antl.WebServer.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        string ExternalId { get; set; }
    }
}