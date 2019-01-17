using Antl.WebServer.Dtos;
using Antl.WebServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using Antl.WebServer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Antl.WebServer.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationHandlerServiceAsync authenticationHandlerServiceAsync;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationHandlerServiceAsync authenticationHandlerServiceAsync, IConfiguration configuration)
        {
            this.authenticationHandlerServiceAsync = authenticationHandlerServiceAsync;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
                return BadRequest(ModelState);

            await authenticationHandlerServiceAsync.RegisterAsync(registerDto).ConfigureAwait(true);

            return new OkObjectResult("Account created");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInRequest)
        {
            if (!ModelState.IsValid || signInRequest == null)
                return BadRequest(ModelState);

            var result = await authenticationHandlerServiceAsync.SignInAsync(signInRequest).ConfigureAwait(true);
            if (!result) throw new UnauthorizedAccessException("Invalid login attempt");

            ApplicationUser user = await authenticationHandlerServiceAsync.GetUserAsync(signInRequest.UserName).ConfigureAwait(true);

            return Ok(RequestToken(user));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await authenticationHandlerServiceAsync.SignOutAsync().ConfigureAwait(true);
            return new OkObjectResult("Sign out Successful");
        }

        private string RequestToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim("EID", user.ExternalId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "http://localhost:44362/api",
                audience: "http://localhost:44362/api",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
