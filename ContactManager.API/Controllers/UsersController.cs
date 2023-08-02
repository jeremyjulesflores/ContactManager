using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Services;
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
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactService _contactService;

        public UsersController(IUserRepository repository,
                               IAuthService authService,       
                               IMapper mapper,
                               ISharedRepository sharedRepository,
                               IContactService contactService)
        {
            this._repository = repository;
            this._authService = authService;
            this._mapper = mapper;
            this._sharedRepository = sharedRepository;
            this._contactService = contactService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreationDto request)
        {
            try
            {
                await _authService.CreateUser(request);
                return Ok("User Successfully Created");
            }
            catch(UserAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500,"Something went wrong");
            }
         
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            try
            {
                var user = await _authService.LogIn(request);

                //if (user.VerifiedAt == null)
                //    {
                //        return BadRequest("Not Verified");
                //    }
                return Ok(user);
            }
            catch(UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidPasswordException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }
            
        }

        //[HttpPost("verify")]
        //public async Task<IActionResult> Verify(string token)
        //{
        //    var user = await _repository.GetUserByToken(token);
        //    if(user == null)
        //    {
        //        return BadRequest("Invalid Token");
        //    }
            
        //    user.VerifiedAt = DateTime.Now;
        //    await _sharedRepository.SaveChangesAsync();

        //    return Ok("User verified!");
        //}

        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword(string email)
        //{
        //    var user = await _repository.GetUserByEmail(email);
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid Token");
        //    }

        //    user.PasswordResetToken = CreateRandomToken();
        //    user.ResetTokenExpires = DateTime.Now.AddHours(3);
        //    await _sharedRepository.SaveChangesAsync();

        //    return Ok("You may now reset your password");
        //}

        //[HttpPost("reset-password")]
        //public async Task<IActionResult> ResetPassword(UserChangePasswordDto request)
        //{
        //    var user = await _repository.GetUserByResetToken(request.Token);
        //    if (user == null || user.ResetTokenExpires < DateTime.Now)
        //    {
        //        return BadRequest("Invalid Token");
        //    }

        //    CreatePasswordHash(request.Password,
        //                       out byte[] passwordHash,
        //                       out byte[] passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;
        //    user.PasswordResetToken = null;
        //    user.ResetTokenExpires = null;
            
        //    await _sharedRepository.SaveChangesAsync();
        //    return Ok("Password Successfully Changed");
        //}

          
    }
}
