using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Cryptography;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreationDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Username = userDto.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto userDto)
        {
            if(user.Username != userDto.UserName)
            {
                return BadRequest("User not Found");
            }

            if(!VerifyPasswordHash(userDto.Password,
                                   user.PasswordHash,
                                   user.PasswordSalt))
            {
                return BadRequest("Incorrect password.");
            }
            return Ok("TOKEN");
        }

        private void CreatePasswordHash(string password, 
                                        out byte[] passwordHash, 
                                        out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
