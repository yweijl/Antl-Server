using System.ComponentModel.DataAnnotations;

namespace Antl.WebServer.Dtos
{
    public class FriendshipDto: IDto
    {
        public int Id { get; set; }

        [Required]
        public string UserIdOne { get; set; }

        [Required]
        public string UserIdTwo { get; set; }
    }
}
