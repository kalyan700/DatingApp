using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _config = config;
            _repository = repository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegister userToRegister)
        {
            userToRegister.Username = userToRegister.Username.ToLower();
            if (await _repository.UserExists(userToRegister.Username)) return BadRequest("Username Already Exists.");

            var userToCreate = new User
            {
                Username = userToRegister.Username
            };
            var createdUser = await _repository.Register(userToCreate, userToRegister.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLogin userToLogin)
        {
            var userFromRepo = await _repository.Login(userToLogin.Username, userToLogin.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescreptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenHandler =  new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescreptor);

            return Ok(new {
                token =tokenHandler.WriteToken(token)
            });


        }
    }
}