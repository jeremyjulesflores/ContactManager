using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public class NumberRepository : INumberRepository
    {
        public Task<Number?> GetNumber(int numberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Number>> GetNumbers()
        {
            throw new NotImplementedException();
        }
    }
}
