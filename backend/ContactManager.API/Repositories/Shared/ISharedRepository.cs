namespace ContactManager.API.Repositories.Shared
{
    public interface ISharedRepository
    {
        Task<bool> SaveChangesAsync();
        Task<bool> ContactExists(int userId, int contactId);
        Task<bool> UserExists(int userId);
    }
}
