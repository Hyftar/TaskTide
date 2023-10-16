using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTideAPI.DTO;
using TaskTideAPI.Repositories;

namespace TaskTideAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IUserRepository UserRepository;

        public AuthenticationController(
            IConfiguration configuration,
            IUserRepository userRepository)
        {
            this.Configuration = configuration;
            this.UserRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO login)
        {
            var user = this.UserRepository.GetByUsername(login.Username);

            if (user == null)
            {
                return this.Unauthorized();
            }

            if (!UserHelpers.Verify(login.Password, user.HashedPassword))
            {
                return this.Unauthorized();
            }

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
                signingCredentials: creds
            );

            return this.Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
