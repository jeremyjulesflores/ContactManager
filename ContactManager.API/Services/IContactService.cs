using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactWithoutDetailsDto>> GetContacts(int userId);
        Task<ContactDto?> GetContact(int contactId, int userId);
        Task<bool> CreateContact(int userId, ContactCreationDto contact);
        Task<bool> DeleteContact(int contactId, int userId);
        Task<bool> UpdateContact(int userId, int contactId, ContactUpdateDto contact);
    }
}
