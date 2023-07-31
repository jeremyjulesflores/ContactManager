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
        Task<bool> DeleteNumber(int contactId, int numberId);
        Task<bool> UpdateNumber(int contactId, int numberId, NumberUpdateDto number);
        Task<NumberUpdateDto> GetNumberToPatch(int contactId, int numberId);
        Task<bool> PatchNumber(int contactId, int numberId, NumberUpdateDto number);
    }
}
