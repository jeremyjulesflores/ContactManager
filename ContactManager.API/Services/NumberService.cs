﻿using AutoMapper;
using ContactManager.API.Entities;
using ContactManager.API.Models;
using ContactManager.API.Models.CreationDtos;
using ContactManager.API.Models.UpdateDtos;
using ContactManager.API.Repositories;
using ContactManager.API.Repositories.Shared;

namespace ContactManager.API.Services
{
    public class NumberService : INumberService
    {
        private readonly INumberRepository _repository;
        private readonly ISharedRepository _sharedRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public NumberService(INumberRepository numberRepository,
                             ISharedRepository sharedRepository,
                             IContactRepository contactRepository,
                             IMapper mapper)
        {
            this._repository = numberRepository;
            this._sharedRepository = sharedRepository;
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }
        public async Task<bool> CreateNumber(int contactId, NumberCreationDto number)
        {
            var contact = await _contactRepository.GetContact(contactId);
            if (contact == null) { return false; }

            var numberToCreate = _mapper.Map<Number>(number);

            this._repository.CreateNumber(contact, numberToCreate);

            return await this._sharedRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAddress(int contactId, int numberId)
        {
            if(!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            if (numberEntity == null) { return false; }
            this._repository.DeleteNumber(numberEntity);

            return await this._sharedRepository.SaveChangesAsync();

        }

        public async Task<NumberDto> GetNumber(int contactId, int numberId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return null;
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            if (numberEntity == null) { return null; }

            return this._mapper.Map<NumberDto>(numberEntity);
        }

        public async Task<IEnumerable<NumberDto>> GetNumbers(int contactId)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return null;
            }
            var numbersEntity = await _repository.GetNumbers(contactId);

            return this._mapper.Map<IEnumerable<NumberDto>>(numbersEntity);
        }

        public async Task<bool> UpdateAddress(int contactId, int numberId, NumberUpdateDto number)
        {
            if (!await _sharedRepository.ContactExists(contactId))
            {
                return false;
            }
            var numberEntity = await _repository.GetNumber(contactId, numberId);
            _mapper.Map(number, numberEntity);

            return await _sharedRepository.SaveChangesAsync();
        }
    }
}
