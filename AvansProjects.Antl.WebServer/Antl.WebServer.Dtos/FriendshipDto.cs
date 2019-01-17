using System.ComponentModel.DataAnnotations;

namespace Antl.WebServer.Dtos
{
    public class FriendshipDto: IDto
    {
        public string ExternalId { get; set; }
        [Required]
        public string FriendId { get; set; }
    }
}
