using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Antl.WebServer.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationHandlerServiceAsync _authenticationHandlerServiceAsync;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationHandlerServiceAsync authenticationHandlerServiceAsync, IConfiguration configuration)
        {
            _authenticationHandlerServiceAsync = authenticationHandlerServiceAsync;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
                return BadRequest(ModelState);

            var result = await _authenticationHandlerServiceAsync.RegisterAsync(registerDto).ConfigureAwait(true);

            return new OkObjectResult(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInRequest)
        {
            if (!ModelState.IsValid || signInRequest == null)
                return BadRequest(ModelState);

            var result = await _authenticationHandlerServiceAsync.SignInAsync(signInRequest).ConfigureAwait(true);
            if (!result) return BadRequest("Unable to login");
            var user = await _authenticationHandlerServiceAsync.GetUserAsync(signInRequest.UserName).ConfigureAwait(true);

            return Ok(RequestToken(user));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _authenticationHandlerServiceAsync.SignOutAsync().ConfigureAwait(true);
            return new OkObjectResult("Sign out Successful");
        }

        private string RequestToken(IEntity user)
        {
            var claims = new[]
            {
                new Claim("EID", user.ExternalId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
#if DEBUG
                "http://localhost:64151",
                "http://localhost:64151",
#else
                "https://antlwebserver.azurewebsites.net",
                "https://antlwebserver.azurewebsites.net",
#endif
                claims,
                expires: DateTime.MaxValue,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
