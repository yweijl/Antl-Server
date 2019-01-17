using System.ComponentModel.DataAnnotations;

namespace Antl.WebServer.Dtos
{
    public class FriendDto : IDto
    {
        public string ExternalId { get; set; }
        public string UserName { get; set; }
    }
}
