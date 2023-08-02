using ContactManager.API.Entities;

namespace ContactManager.API.Repositories
{
    public interface INumberRepository
    {
        /// <summary>
        /// Get all Numbers
        /// </summary>
        /// <param name="contactId">Id of contact where numbers will come from</param>
        /// <returns>IEnumeralbe of Numbers</returns>
        Task<IEnumerable<Number>> GetNumbers(int contactId);
        /// <summary>
        /// Get Number
        /// </summary>
        /// <param name="contactId">Id of contact where number will come from</param>
        /// <param name="numberId">Id of number to get</param>
        /// <returns>Number Object</returns>
        Task<Number?> GetNumber(int contactId, int numberId);
        /// <summary>
        /// Creates a number
        /// </summary>
        /// <param name="contact">Contact Object to add Number to</param>
        /// <param name="number">Number object to add</param>
        void CreateNumber(Contact contact, Number number);
        /// <summary>
        /// Delete number
        /// </summary>
        /// <param name="number">Number object to delete</param>
        void DeleteNumber(Number number);
    }
}
