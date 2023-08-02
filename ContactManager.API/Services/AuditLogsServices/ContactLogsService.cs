using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Repositories.AuditLogsRepository;
using ContactManager.API.Repositories.Shared;

namespace ContactManager.API.Services.AuditLogsServices
{
    public interface IContactLogsService
    {
        void CreateLog(string action, string userName, string contactName, string details);
    }
    public class ContactLogsService : IContactLogsService
    {
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactLogsRepository _repository;
        private readonly IMapper _mapper;

        public ContactLogsService(ISharedRepository sharedRepository,
                              IContactLogsRepository repository,
                              IMapper mapper)
        {
            this._sharedRepository = sharedRepository;
            this._repository = repository;
            this._mapper = mapper;
        }
        public void CreateLog(string action, string userName, string contactName, string details)
        {
            ContactLogDto log = new ContactLogDto
            {
                Action = action,
                UserName = userName,
                ContactName = contactName,
                Details = details,
                TimeStamp = DateTime.UtcNow
            };

            var logToCreate = _mapper.Map<ContactLogs>(log);
            _repository.CreateLog(logToCreate);
            _sharedRepository.SaveChangesAsync();
        }
    }
}
