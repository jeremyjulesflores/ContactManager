using ContactManager.API.DbContexts;
using ContactManager.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactInfoContext _context;
 

        public UserRepository(ContactInfoContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        async Task IUserRepository.CreateUser(User user)
        {
            _context.User.Add(user);
        }

        async Task<bool> IUserRepository.EmailExists(string email)
        {
            return await _context.User.AnyAsync(u=> u.Email == email);
        }

        async Task<IEnumerable<User>> IUserRepository.GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        async Task<User?> IUserRepository.GetUser(int userId)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
        }

        async Task<User?> IUserRepository.GetUserByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        async Task<User?> IUserRepository.GetUserByResetToken(string token)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
        }

        async Task<User?> IUserRepository.GetUserByToken(string token)
        {
            return await _context.User.FirstOrDefaultAsync(u=>u.VerificationToken == token);
        }

        async Task<User?> IUserRepository.GetUserByUsername(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == username);
        }

        async Task<bool> IUserRepository.UsernameExists(string username)
        {
            return await _context.User.AnyAsync(u => u.Username == username);
        }
    }

}
