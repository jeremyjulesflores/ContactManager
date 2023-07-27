using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(int userId);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserByToken(string token);
        Task<User?> GetUserByResetToken(string token);
        Task<bool> UsernameExists(string username);
        Task<bool> EmailExists(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task CreateUser(User user);
        
    }
}
