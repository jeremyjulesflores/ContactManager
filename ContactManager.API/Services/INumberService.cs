using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Services
{
    public interface INumberService
    {
        Task<IEnumerable<NumberDto>> GetNumbers(int contactId);
        Task<NumberDto> GetNumber(int contactId, int numberId);
        Task<bool> CreateNumber (int contactId, NumberCreationDto number);
        Task<bool> DeleteAddress(int contactId, int numberId);
        Task<bool> UpdateAddress(int contactId, int numberId, NumberUpdateDto address);
    }
}
