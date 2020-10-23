using Finflow.Hlopov.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientModel>> GetClientList();
        Task<IEnumerable<ClientModel>> GetClientsByName(string name);
        Task<ClientModel> GetClientById(int clientId);
        Task<ClientModel> Create(ClientModel clientModel);
        Task Update(ClientModel clientModel);
        Task Delete(ClientModel clientModel);
    }
}