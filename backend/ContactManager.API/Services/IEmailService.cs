﻿using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using Microsoft.AspNetCore.JsonPatch;

namespace ContactManager.API.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Gets all Email by contact Id
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <returns>An IEnumerable of EmailDto</returns>
        Task<IEnumerable<EmailDto>> GetEmails(int userId, int contactId);
        /// <summary>
        /// Gets one Email
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="emailId"></param>
        /// <param name="contactId"></param>
        /// <returns>An AdressDto Object</returns>
        Task<EmailDto?> GetEmail(int userId, int contactId, int emailId);
        /// <summary>
        /// Creates Email
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <param name="address"></param>
        /// <returns>bool if create is successful or not</returns>
        Task CreateEmail(int userId, int contactId, EmailCreationDto address);
        /// <summary>
        /// Deletes an Email
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <param name="emailId"></param>
        /// <returns>bool if address is successful or not</returns>
        Task DeleteEmail(int userId, int contactId, int emailId);
        /// <summary>
        /// Updates and Email
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <param name="address"></param>
        /// <returns>bool if address is successful or not</returns>
        Task UpdateEmail(int userId, int contactId, int emailId, EmailUpdateDto address);
        /// <summary>
        /// Get the Email to Patch
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <param name="emailId"></param>
        /// <returns>Email Update Dto</returns>
        Task<EmailUpdateDto> GetEmailToPatch(int userId, int contactId, int emailId);
        /// <summary>
        /// Patch Email
        /// </summary>
        /// <param name="userId">Id of user </param>
        /// <param name="contactId"></param>
        /// <param name="emailId"></param>
        /// <param name="address"></param>
        /// <returns>Bool if the address has been patch</returns>
        Task PatchNumber(int userId, int contactId, int emailId, EmailUpdateDto address);
    }

}
