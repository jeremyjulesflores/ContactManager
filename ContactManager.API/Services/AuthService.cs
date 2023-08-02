using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ContactManager.API.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactService _contactService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository,
                           IMapper mapper,
                           ISharedRepository sharedRepository,
                           IContactService contactService,
                           IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._sharedRepository = sharedRepository;
            this._contactService = contactService;
            this._configuration = configuration;
        }
        public async Task<PasswordHashResult> CreatePasswordHash(string password)
        {
            return await Task.Run(() =>
            {
                using (var hmac = new HMACSHA512())
                {
                    var passwordSalt = hmac.Key;
                    var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                    return new PasswordHashResult { PasswordHash = passwordHash, PasswordSalt = passwordSalt };
                }
            });
        }

        public async Task CreateUser(UserCreationDto request)
        {
            if (await _userRepository.EmailExists(request.Email) || await _userRepository.UsernameExists(request.UserName))
            {
                throw new UserAlreadyExistsException("User Already Exists");
            }

            PasswordHashResult result = await CreatePasswordHash(request.Password);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.UserName,
                PasswordHash = result.PasswordHash,
                PasswordSalt = result.PasswordSalt
            };

            await _userRepository.CreateUser(user);

            if(await _sharedRepository.SaveChangesAsync())
            {
                await _contactService.CreateContact(user.Id, new ContactCreationDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName    
                });
            }
        }

        public async Task<string> LogIn(UserLoginDto loginRequest)
        {
            var user = await _userRepository.GetUserByUsername(loginRequest.UserName);
            if(user == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            if(! await VerifyPasswordHash(loginRequest.Password,
                        user.PasswordHash,
                        user.PasswordSalt))
            {
                throw new InvalidPasswordException("Invalid password.");
            }

            string token = await CreateToken(user);
            return token;
        }

        public async Task<bool> VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return await Task.Run(() =>
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(passwordHash);
                }
            });
        }

        public async Task<string> CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = await Task.Run(() =>
            {
                return new JwtSecurityTokenHandler().WriteToken(token);
            });

            return jwt;
        }
    }

    public class PasswordHashResult
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
