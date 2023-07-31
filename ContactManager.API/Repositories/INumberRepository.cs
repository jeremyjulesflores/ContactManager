using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface INumberRepository
    {
        Task<IEnumerable<Number>> GetNumbers(int contactId);
        Task<Number?> GetNumber(int contactId, int numberId);
        void CreateNumber(Contact contact, Number number);
        void DeleteNumber(Number number);
    }
}
