using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Interfaces;
using Finflow.Hlopov.Core.Repositories;
using Finflow.Hlopov.Application.Models;
using Finflow.Hlopov.Application.Mapper;
using Finflow.Hlopov.Application.Interfaces;

namespace Finflow.Hlopov.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IApplicationLogger<ClientService> _logger;

        public ClientService(IClientRepository clientepository, IApplicationLogger<ClientService> logger)
        {
            _clientRepository = clientepository ?? throw new ArgumentNullException(nameof(clientepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ClientModel>> GetClientList()
        {
            var clientList = await _clientRepository.GetClientListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ClientModel>>(clientList);
            return mapped;
        }

        public async Task<IEnumerable<ClientModel>> GetClientsByName(string name)
        {
            var clientList = await _clientRepository.GetClientsByNameAsync(name);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ClientModel>>(clientList);
            return mapped;
        }

        public async Task<ClientModel> GetClientById(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);
            var mapped = ObjectMapper.Mapper.Map<ClientModel>(client);
            return mapped;
        }

        public async Task<ClientModel> Create(ClientModel clientModel)
        {
            await ValidateClientIfExist(clientModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Client>(clientModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Entity could not be mapped.");

            var newEntity = await _clientRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Entity successfully added.");

            var newMappedEntity = ObjectMapper.Mapper.Map<ClientModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(ClientModel clientModel)
        {
            await ValidateClientIfNotExist(clientModel);

            var editClient = await _clientRepository.GetByIdAsync(clientModel.Id);
            if (editClient == null)
                throw new ApplicationException($"Entity could not be loaded.");

            ObjectMapper.Mapper.Map(clientModel, editClient);

            await _clientRepository.UpdateAsync(editClient);
            _logger.LogInformation($"Entity successfully updated.");
        }

        public async Task Delete(ClientModel clientModel)
        {
            await ValidateClientIfNotExist(clientModel);
            var deletedClient = await _clientRepository.GetByIdAsync(clientModel.Id);
            if (deletedClient == null)
                throw new ApplicationException($"Entity could not be loaded.");

            await _clientRepository.DeleteAsync(deletedClient);
            _logger.LogInformation($"Entity successfully deleted.");
        }

        private async Task ValidateClientIfExist(ClientModel clientModel)
        {
            var existingEntity = await _clientRepository.GetByIdAsync(clientModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{clientModel.ToString()} with this id already exists.");
        }

        private async Task ValidateClientIfNotExist(ClientModel clientModel)
        {
            var existingEntity = await _clientRepository.GetByIdAsync(clientModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{clientModel.ToString()} with this id does not exist.");
        }
    }
}
