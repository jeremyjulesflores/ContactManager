using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="userId">user of id</param>
        /// <returns>User object</returns>
        Task<User?> GetUser(int userId);
        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <returns>User object</returns>
        Task<User?> GetUserByEmail(string email);
        /// <summary>
        /// Get a User by username
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>User Object</returns>
        Task<User?> GetUserByUsername(string username);
        /// <summary>
        /// Get a User by token
        /// </summary>
        /// <param name="token">token of user</param>
        /// <returns>User Object</returns>
        Task<User?> GetUserByToken(string token);
        /// <summary>
        /// Get a User by Reset token
        /// </summary>
        /// <param name="token">Reset token of user</param>
        /// <returns>User object</returns>
        Task<User?> GetUserByResetToken(string token);
        /// <summary>
        /// Checks if username already exists
        /// </summary>
        /// <param name="username">Username to check</param>
        /// <returns>bool whether or not username exists or not</returns>
        Task<bool> UsernameExists(string username);
        /// <summary>
        /// Checks if email already exists
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>bool whther or not email exists or not</returns>
        Task<bool> EmailExists(string email);
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>IEnumerable of users</returns>
        Task<IEnumerable<User>> GetAllUsers();
        /// <summary>
        /// Creates a User
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns></returns>
        Task CreateUser(User user);
        
    }
}
