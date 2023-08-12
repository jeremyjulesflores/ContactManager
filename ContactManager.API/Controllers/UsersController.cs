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
        private readonly IAuthService _authService;

        public UsersController(IAuthService authService)
        {
            this._authService = authService;

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
                var token = await _authService.LogIn(request);

                return Ok(token);
            }
            catch(UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
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

        [HttpPost("check")]
        public async Task<IActionResult> Check (TokenUserCheckDto request)
        {
            try
            {
                var isValid =  await Task.Run(()=> _authService.Check(request));

                if (isValid)
                {
                    return Ok("Token Valid");
                }

                return BadRequest("Token Invalid");
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
