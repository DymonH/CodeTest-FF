using Finflow.Hlopov.Application.Interfaces;
using Finflow.Hlopov.Application.Mapper;
using Finflow.Hlopov.Application.Models;
using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Interfaces;
using Finflow.Hlopov.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Application.Services
{
    public class RemmittanceService : IRemmittanceService
    {
        private readonly IRemmittanceRepository _remmittanceRepository;
        private readonly IApplicationLogger<RemmittanceService> _logger;

        public RemmittanceService(IRemmittanceRepository remmittanceRepository, IApplicationLogger<RemmittanceService> logger)
        {
            _remmittanceRepository = remmittanceRepository;
            _logger = logger;
        }

        public async Task<RemmittanceModel> Create(RemmittanceModel remmittanceModel)
        {
            await ValidateIfExist(remmittanceModel);

            try
            {
                var mappedEntity = ObjectMapper.Mapper.Map<Remittance>(remmittanceModel);
                if (mappedEntity == null)
                    throw new ApplicationException($"Entity could not be mapped.");

                var newEntity = await _remmittanceRepository.Create(mappedEntity);
                _logger.LogInformation($"Entity successfully added.");

                var newMappedEntity = ObjectMapper.Mapper.Map<RemmittanceModel>(newEntity);
                return newMappedEntity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RemmittanceModel> GetRemmittanceById(Guid remmittanceId)
        {
            var remmittance = await _remmittanceRepository.GeRemittanceByIdAsync(remmittanceId);
            var mapped = ObjectMapper.Mapper.Map<RemmittanceModel>(remmittance);
            return mapped;
        }

        public async Task<IEnumerable<RemmittanceModel>> GetRemmittanceList()
        {
            var list = await _remmittanceRepository.GetRemittanceListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<RemmittanceModel>>(list);
            return mapped;
        }

        public async Task<RemmittanceModel> UpdateStatus(Guid remmittanceId, int statusId)
        {
            await ValidateIfNotExist(remmittanceId);
            var updated = await _remmittanceRepository.UpdateStatusAsync(remmittanceId, statusId);
            var mapped = ObjectMapper.Mapper.Map<RemmittanceModel>(updated);
            return mapped;
        }

        private async Task ValidateIfExist(RemmittanceModel remmittanceModel)
        {
            var existingEntity = await _remmittanceRepository.GetByIdAsync(remmittanceModel.Id);
            if (existingEntity != null)
                throw new ApplicationException("Remmittance with this id already exists.");
        }

        private async Task ValidateIfNotExist(Guid id)
        {
            var existingEntity = await _remmittanceRepository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new ApplicationException("Remmittance with this id does not exist.");
        }
    }
}
