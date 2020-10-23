using Finflow.Hlopov.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Application.Interfaces
{
    public interface IRemmittanceService
    {
        Task<IEnumerable<RemmittanceModel>> GetRemmittanceList();
        Task<RemmittanceModel> GetRemmittanceById(Guid remmittanceId);
        Task<RemmittanceModel> Create(RemmittanceModel remmittanceModel);
        Task<RemmittanceModel> UpdateStatus(Guid remmittanceId, int status);
    }
}