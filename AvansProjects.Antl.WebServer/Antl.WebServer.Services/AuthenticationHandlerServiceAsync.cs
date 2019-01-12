using AgileObjects.AgileMapper;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
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

        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var user = Mapper.Map(registerDto).ToANew<ApplicationUser>();
            user.ExternalId = string.Join("-", new Random().Next(1000, 9999).ToString(),
                new Random().Next(1000, 9999).ToString(), new Random().Next(1000, 9999).ToString());

            var result = await _userManager.CreateAsync(user, registerDto.Password).ConfigureAwait(true);
            if (!result.Succeeded) throw new ArgumentNullException(nameof(result));

            await _userManager.AddToRoleAsync(user, "User").ConfigureAwait(false);
            await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(false);
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
