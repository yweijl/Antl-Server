using System.Threading.Tasks;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Interfaces
{
    public interface IAuthenticationHandlerService
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<bool> SignInAsync(SignInDto signInDto);
        Task SignOutAsync();
    }
}