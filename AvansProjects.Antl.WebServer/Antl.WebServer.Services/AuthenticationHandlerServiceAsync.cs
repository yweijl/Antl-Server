using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Antl.WebServer.Services
{
    public class AuthenticationHandlerServiceAsyncAsync : IAuthenticationHandlerServiceAsync
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationHandlerServiceAsyncAsync(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var user = Mapper.Map(registerDto).ToANew<ApplicationUser>();
            user.ExternalId = Generate.ExternalId();

            var result = await _userManager.CreateAsync(user, registerDto.Password).ConfigureAwait(false);
            if (!result.Succeeded) return "Something went wrong while registering";

            await _userManager.AddToRoleAsync(user, "User").ConfigureAwait(false);
            await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
            return "Account created";
        }

        public Task<bool> SignInAsync(SignInDto signInDto)
        {
            return _signInManager
                .PasswordSignInAsync(signInDto.UserName, signInDto.Password, isPersistent: false,
                    lockoutOnFailure: false).ContinueWith(x => x.Result.Succeeded);
        }

        public Task<ApplicationUser> GetUserAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }

}
