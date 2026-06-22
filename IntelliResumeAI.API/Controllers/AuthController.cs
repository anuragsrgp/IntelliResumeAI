using IntelliResumeAI.API.Data;
using IntelliResumeAI.API.DTOs;
using IntelliResumeAI.API.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IntelliResumeAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IConfiguration _configuration;

        public AuthController(
            AppDbContext context,
            IConfiguration configuration
        )
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("IntelliResumeAI API Working");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var existingUser =
                _context.Users.FirstOrDefault(
                    x => x.Email == dto.Email
                );

            if (existingUser != null)
            {
                return BadRequest(
                    "Email already exists"
                );
            }

            var user = new User
            {
                Name = dto.Name,

                Email = dto.Email,

                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(
                        dto.Password
                    ),

                CreatedDate =
                    DateTime.Now
            };

            _context.Users.Add(user);

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Registration Successful"
            });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user =
                _context.Users.FirstOrDefault(
                    x => x.Email == dto.Email
                );

            if (user == null)
            {
                return BadRequest(
                    "Invalid Email"
                );
            }

            bool isValidPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash
                );

            if (!isValidPassword)
            {
                return BadRequest(
                    "Invalid Password"
                );
            }

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Name
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                )
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!
                    )
                );

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        DateTime.Now.AddHours(2),

                    signingCredentials:
                        credentials
                );

            return Ok(new
            {
                Token =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token),

                user.Id,

                user.Name,

                user.Email
            });
        }
    }
}