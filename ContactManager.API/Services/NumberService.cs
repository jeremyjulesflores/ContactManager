using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Helper;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;
using ContactManager.API.Services.AuditLogsServices;

namespace ContactManager.API.Services
{
    public class NumberService : INumberService
    {
        private readonly INumberRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContactLogsService _contactLogsService;
        private readonly IMapper _mapper;

        public NumberService(INumberRepository numberRepository,
                             ISharedRepository sharedRepository,
                             IContactRepository contactRepository,
                             IUserRepository userRepository,
                             IContactLogsService contactLogsService,
                             IMapper mapper)
        {
            this._repository = numberRepository;
            this._sharedRepository = sharedRepository;
            this._contactRepository = contactRepository;
            this._userRepository = userRepository;
            this._contactLogsService = contactLogsService;
            this._mapper = mapper;
        }
        public async Task CreateNumber(int userId, int contactId, NumberCreationDto number)
        {
            var user = await _userRepository.GetUser(userId);
            if(user == null)
            {
                throw new UserNotFoundException("User not Found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not Found"); 
            }
            var numberToCreate = _mapper.Map<Number>(number);

            this._repository.CreateNumber(contact, numberToCreate);
            if(!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Create");
            }
            _contactLogsService.CreateLog("Create", user.Username, $"{contactId} : {contact.FirstName} {contact.LastName}",
                                           $"Created {number.Type} number {numberToCreate.Id} : {number.ContactNumber}");
        }

        public async Task DeleteNumber(int userId, int contactId, int numberId)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not Found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not Found");
            }

            var numberEntity = await _repository.GetNumber(contactId, numberId);
            if(numberEntity == null) 
            { 
                throw new ArgumentNullException("Number not found");
            }
            this._repository.DeleteNumber(numberEntity);
            
            if (!await this._sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed Deleting Number");
            }
            _contactLogsService.CreateLog("Delete", user.Username,
                                         $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                         $"Deleted {numberEntity.Type} number {numberId} : {numberEntity.ContactNumber}");

        }

        public async Task<NumberDto> GetNumber(int userId, int contactId, int numberId)
        {
            if(!await _sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User not found");
            }
            if (!await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact not found");
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            if (numberEntity == null)
            {
                throw new ArgumentNullException(nameof(numberEntity));
            }

            return this._mapper.Map<NumberDto>(numberEntity);
        }

        public async Task<IEnumerable<NumberDto>> GetNumbers(int userId, int contactId)
        {
            if(!await _sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User not found");
            }
            if (!await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact not found");
            }
            var numbersEntity = await _repository.GetNumbers(contactId);
            if(numbersEntity == null)
            {
                throw new ArgumentNullException(nameof(numbersEntity));
            }
            return this._mapper.Map<IEnumerable<NumberDto>>(numbersEntity);
        }

        public async Task UpdateNumber(int userId, int contactId, int numberId, NumberUpdateDto number)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not found");
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            if (numberEntity == null)
            {
                throw new ArgumentNullException(nameof(numberEntity));
            }
            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences(numberEntity, number);
            foreach (var differenceEntity in difference)
            {
                details += $"[{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue}];\n";
            }
            _mapper.Map(number, numberEntity);
            if (!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to update number");
            }
            
            _contactLogsService.CreateLog("Update", user.Username,
                                        $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                        $"Number {numberEntity.Id} Updated \n" + details);

        }

        async Task<NumberUpdateDto> INumberService.GetNumberToPatch(int userId, int contactId, int numberId)
        {
            if (!await _sharedRepository.UserExists(userId))
            {
                throw new UserNotFoundException("User not Found");
            }
            if (!await _sharedRepository.ContactExists(userId, contactId))
            {
                throw new ContactNotFoundException("Contact not Found");
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);

            return _mapper.Map<NumberUpdateDto>(numberEntity);
        }

        async Task INumberService.PatchNumber(int userId, int contactId, int numberId, NumberUpdateDto number)
        {
            var user = await _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }
            var contact = await _contactRepository.GetContact(userId, contactId);
            if (contact == null)
            {
                throw new ContactNotFoundException("Contact not found");
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            string details = String.Empty;
            var difference = GetObjectDifference.GetObjectDifferences<Number, NumberUpdateDto>(numberEntity, number);
            foreach (var differenceEntity in difference)
            {
                details += $"{differenceEntity.PropertyName} : From: {differenceEntity.OriginalValue} -> {differenceEntity.ChangedValue};\n";
            }
            if (!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to update number");
            }
            _mapper.Map(number, numberEntity);

            if (!await _sharedRepository.SaveChangesAsync())
            {
                throw new Exception("Failed to Patch Email");
            }

            _contactLogsService.CreateLog("Patch", user.Username,
                                        $"{contact.Id} : {contact.FirstName} {contact.LastName}",
                                        $"Number {numberEntity.Id} Patched \n" + details);
        }
    }
}
