using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NodaTime;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTideAPI.DTO;
using TaskTideAPI.Models;
using TaskTideAPI.Repositories;

namespace TaskTideAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IUserRepository UserRepository;
        private readonly ZonedClock Clock;

        public AuthenticationController(
            IConfiguration configuration,
            IUserRepository userRepository,
            ZonedClock clock)
        {
            this.Configuration = configuration;
            this.UserRepository = userRepository;
            this.Clock = clock;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO login)
        {
            var user = this.UserRepository.GetByUsername(login.Username);

            if (user == null || !UserHelpers.Verify(login.Password, user.HashedPassword))
            {
                return this.Unauthorized(new { Errors = new[] { "Invalid username or password" } });
            }

            var token = this.CreateToken(user);

            return this.Json(new { token });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterDTO login)
        {
            var user = this.UserRepository.CreateUser(login.Username, login.Password);

            var token = this.CreateToken(user);

            return this.Json(new { token });
        }

        private string CreateToken(User user)
        {
            var claims =
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: this.Configuration["JWT:Issuer"],
                audience: this.Configuration["JWT:Audience"],
                claims,
                expires: DateTime.MaxValue,
                notBefore: this.Clock.GetCurrentZonedDateTime().ToDateTimeUtc(),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
