using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;

namespace ContactManager.API.Services
{
    public interface IAuthService
    {
        Task<PasswordHashResult> CreatePasswordHash(string password);
        Task CreateUser(UserCreationDto request);
        Task<string> LogIn(UserLoginDto loginRequest);
        Task<bool> VerifyPasswordHash(string password,
                                      byte[] passwordHash,
                                      byte[] passwordSalt);
        Task<string> CreateToken(User user);
        bool Check(TokenUserCheckDto request);

           
    }
}
