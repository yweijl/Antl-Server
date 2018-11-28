using System.Threading.Tasks;
using Antl.WebServer.Dtos;

namespace Antl.WebServer.Interfaces
{
    public interface IAuthenticationHandlerServiceAsync
    {
        Task RegisterAsync(RegisterDto registerDto);
        Task<bool> SignInAsync(SignInDto signInDto);
        Task SignOutAsync();
    }
}