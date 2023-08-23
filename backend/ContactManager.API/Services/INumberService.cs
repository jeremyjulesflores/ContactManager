using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;

namespace ContactManager.API.Services
{
    public interface INumberService
    {
        /// <summary>
        /// Get all Numbers
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="userId">Id of user </param>
        /// <returns>IEnumerable of Number Objects</returns>
        Task<IEnumerable<NumberDto>> GetNumbers(int userId, int contactId);
        /// <summary>
        /// Get Number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="userId">Id of user </param>
        /// <returns>Number Object</returns>
        Task<NumberDto> GetNumber(int userId, int contactId, int numberId);
        /// <summary>
        /// Creates a Number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="number"></param>
        /// <param name="userId">Id of user </param>
        /// <returns>True : Created, False : Failed</returns>
        Task CreateNumber (int userId, int contactId, NumberCreationDto number);
        /// <summary>
        /// Deletes a number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="userId">Id of user </param>
        /// <returns></returns>
        Task DeleteNumber(int userId, int contactId, int numberId);
        /// <summary>
        /// Updates a number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="userId">Id of user </param>
        /// <param name="number"></param>
        /// <returns></returns>
        Task UpdateNumber(int userId, int contactId, int numberId, NumberUpdateDto number);
        /// <summary>
        /// Gets a number for patching
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <returns></returns>
        Task<NumberUpdateDto> GetNumberToPatch(int userId, int contactId, int numberId);
        /// <summary>
        /// Patches the number
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="number">Updated Number Object</param>
        /// <returns></returns>
        Task PatchNumber(int userId, int contactId, int numberId, NumberUpdateDto number);
    }
}
