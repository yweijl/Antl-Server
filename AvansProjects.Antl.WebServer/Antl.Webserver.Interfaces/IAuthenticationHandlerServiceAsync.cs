using System;
using System.Threading.Tasks;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;

namespace Antl.WebServer.Interfaces
{
    public interface IAuthenticationHandlerServiceAsync
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<bool> SignInAsync(SignInDto signInDto);
        Task<ApplicationUser> GetUserAsync(string userName);
        Task SignOutAsync();
    }
}