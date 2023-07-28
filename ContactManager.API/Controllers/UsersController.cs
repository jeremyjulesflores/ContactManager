using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using System.Security.Cryptography;

namespace ContactManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISharedRepository _sharedRepository;

        public UsersController(IUserRepository repository,
                               IMapper mapper,
                               ISharedRepository sharedRepository)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._sharedRepository = sharedRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreationDto request)
        {
            if (await _repository.EmailExists(request.Email))
            {
                return BadRequest("Email already exists");
            }
            if(await _repository.UsernameExists(request.UserName))
            {
                return BadRequest("Username already exists");
            }

            CreatePasswordHash(request.Password,
                               out byte[] passwordHash,
                               out byte[] passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };
            
            await _repository.CreateUser(user);
            await _sharedRepository.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var user = await _repository.GetUserByUsername(request.UserName);
            if (user == null)
            {
                //change later : Too much information
                // maybe : Username or Password is incorrect
                return BadRequest("Username is incorrect");
            }

            

            if (!VerifyPasswordHash(request.Password,
                                   user.PasswordHash,
                                   user.PasswordSalt))
            {
                //change later : Too much information
                // maybe : Username or Password is incorrect
                return BadRequest("Incorrect Password");
            }

            if (user.VerifiedAt == null)
                {
                    return BadRequest("Not Verified");
                }

            return Ok($"Welcome back, {user.FirstName}");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await _repository.GetUserByToken(token);
            if(user == null)
            {
                return BadRequest("Invalid Token");
            }
            
            user.VerifiedAt = DateTime.Now;
            await _sharedRepository.SaveChangesAsync();

            return Ok("User verified!");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user == null)
            {
                return BadRequest("Invalid Token");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddHours(3);
            await _sharedRepository.SaveChangesAsync();

            return Ok("You may now reset your password");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(UserChangePasswordDto request)
        {
            var user = await _repository.GetUserByResetToken(request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token");
            }

            CreatePasswordHash(request.Password,
                               out byte[] passwordHash,
                               out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;
            
            await _sharedRepository.SaveChangesAsync();
            return Ok("Password Successfully Changed");
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

        private bool VerifyPasswordHash(string password,
                                        byte[] passwordHash,
                                        byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
