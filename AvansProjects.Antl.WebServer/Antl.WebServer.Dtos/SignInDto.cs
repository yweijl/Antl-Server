using System.ComponentModel.DataAnnotations;

namespace Antl.WebServer.Dtos
{
    public class SignInDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
