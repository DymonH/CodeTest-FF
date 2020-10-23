using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Core.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientListAsync();
        Task<IEnumerable<Client>> GetClientsByNameAsync(string clientName);
        Task<Client> GetClientByIdAsync(int clientId);
    }
}