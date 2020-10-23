using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Core.Repositories
{
    public interface IRemmittanceRepository: IRepository<Remittance>
    {
        Task<IEnumerable<Remittance>> GetRemittanceListAsync();
        Task<Remittance> GeRemittanceByIdAsync(Guid remmittanceId);
        Task<Remittance> UpdateStatusAsync(Guid remmittanceId, int statusId);
        Task<Remittance> Create(Remittance remittance);
    }
}