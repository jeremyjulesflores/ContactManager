using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface INumberRepository
    {
        Task<IEnumerable<Number>> GetNumbers();
        Task<Number?> GetNumber(int numberId);
    }
}
