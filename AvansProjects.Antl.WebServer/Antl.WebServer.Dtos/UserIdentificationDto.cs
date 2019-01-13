using System;
using System.ComponentModel.DataAnnotations;

namespace Antl.WebServer.Dtos
{
    public class UserIdentificationDto : IDto
    {
        [Required]
        public string ExternalId { get; set; }
        public int Id { get; set; }
    }
}