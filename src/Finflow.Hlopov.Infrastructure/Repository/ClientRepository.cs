using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Repositories;
using Finflow.Hlopov.Core.Specifications;
using Finflow.Hlopov.Infrastructure.Data;
using Finflow.Hlopov.Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Infrastructure.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(FinflowContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Client>> GetClientListAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsByNameAsync(string clientName)
        {
            var spec = new ClientSpecification(clientName);
            return await GetAsync(spec);
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            var spec = new ClientSpecification(clientId);
            var client = (await GetAsync(spec)).FirstOrDefault();
            return client;
        }
    }
}