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

namespace Antl.WebServer.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationHandlerService _authenticationHandlerService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationHandlerService authenticationHandlerService, IConfiguration configuration)
        {
            _authenticationHandlerService = authenticationHandlerService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
                return BadRequest(ModelState);

            await _authenticationHandlerService.RegisterAsync(registerDto).ConfigureAwait(true);

            return new OkObjectResult("Account created");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto signInRequest)
        {
            if (!ModelState.IsValid || signInRequest == null)
                return BadRequest(ModelState);

            var result = await _authenticationHandlerService.SignInAsync(signInRequest).ConfigureAwait(true);
            if (!result) throw new UnauthorizedAccessException("Invalid login attempt");

            return Ok(RequestToken(signInRequest.UserName));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _authenticationHandlerService.SignOutAsync().ConfigureAwait(true);
            return new OkObjectResult("Sign out Successful");
        }

        private string RequestToken(string userName)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
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
