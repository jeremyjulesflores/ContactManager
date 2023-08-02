﻿using ContactManager.API.DbContexts;
using ContactManager.API.Entities;

namespace ContactManager.API.Repositories.AuditLogsRepository
{
    public interface IContactLogsRepository
    {
        void CreateLog(ContactLogs log);
    }
    public class ContactLogsRepository : IContactLogsRepository
    {
        private readonly ContactInfoContext _context;

        public ContactLogsRepository(ContactInfoContext context)
        {
            this._context = context;
        }

        public void CreateLog(ContactLogs log)
        {
            _context.ContactLogs.Add(log);
        }
    }
}
