using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Repositories.AuditLogsRepository;
using ContactManager.API.Repositories.Shared;

namespace ContactManager.API.Services.AuditLogsServices
{
    public interface IUserLogsService
    {
        void CreateLog(string action, string userName, string details);
    }

    public class UserLogsService : IUserLogsService
    {
        private readonly ISharedRepository _sharedRepository;
        private readonly IUserLogsRepository _repository;
        private readonly IMapper _mapper;

        public UserLogsService(ISharedRepository sharedRepository,
                              IUserLogsRepository repository,
                              IMapper mapper)
        {
            this._sharedRepository = sharedRepository;
            this._repository = repository;
            this._mapper = mapper;
        }

        void IUserLogsService.CreateLog(string action, string userName, string details)
        {
            UserLogDto log = new UserLogDto
            {
                Action = action,
                UserName = userName,
                Details = details,
                TimeStamp = DateTime.UtcNow
            };

            var logToCreate = _mapper.Map<UserLogs>(log);
            _repository.CreateLog(logToCreate);
            _sharedRepository.SaveChangesAsync();
        }
    }
}
