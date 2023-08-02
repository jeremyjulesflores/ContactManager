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
        /// <returns>IEnumerable of Number Objects</returns>
        Task<IEnumerable<NumberDto>> GetNumbers(int contactId);
        /// <summary>
        /// Get Number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <returns>Number Object</returns>
        Task<NumberDto> GetNumber(int contactId, int numberId);
        /// <summary>
        /// Creates a Number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="number"></param>
        /// <returns>True : Created, False : Failed</returns>
        Task<bool> CreateNumber (int contactId, NumberCreationDto number);
        /// <summary>
        /// Deletes a number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <returns></returns>
        Task<bool> DeleteNumber(int contactId, int numberId);
        /// <summary>
        /// Updates a number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<bool> UpdateNumber(int contactId, int numberId, NumberUpdateDto number);
        /// <summary>
        /// Gets a number for patching
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <returns></returns>
        Task<NumberUpdateDto> GetNumberToPatch(int contactId, int numberId);
        /// <summary>
        /// Patches the number
        /// </summary>
        /// <param name="contactId">Id of contact </param>
        /// <param name="numberId">Id of Number to process</param>
        /// <param name="number">Updated Number Object</param>
        /// <returns></returns>
        Task<bool> PatchNumber(int contactId, int numberId, NumberUpdateDto number);
    }
}
