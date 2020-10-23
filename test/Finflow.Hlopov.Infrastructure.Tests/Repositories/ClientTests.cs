using Finflow.Hlopov.Infrastructure.Data;
using Finflow.Hlopov.Infrastructure.Repository;
using Finflow.Hlopov.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Finflow.Hlopov.Infrastructure.Tests.Repositories
{
    public class ClientTests
    {
        private readonly FinflowContext _dbContext;
        private readonly ClientRepository _repository;
        private readonly ITestOutputHelper _output;
        private ClientBuilder ClientBuilder { get; } = new ClientBuilder();
        
        public ClientTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<FinflowContext>()
                .UseInMemoryDatabase(databaseName: "Finflow")
                .Options;

            _dbContext = new FinflowContext(dbOptions);
            _repository = new ClientRepository(_dbContext);
        }

        [Fact]
        public async Task Get_Existing_Client()
        {
            var existingClient = ClientBuilder.Build();
            _dbContext.Clients.Add(existingClient);
            await _dbContext.SaveChangesAsync();

            var clientId = existingClient.Id;
            _output.WriteLine($"ClientId: { clientId }");

            var clientFromRepo = await _repository.GetByIdAsync(clientId);
            Assert.Equal(ClientBuilder.ClientId, clientFromRepo.Id);
            Assert.Equal(ClientBuilder.ClientName, clientFromRepo.Name);
            Assert.Equal(ClientBuilder.ClientSurname, clientFromRepo.Surname);
            Assert.Equal(ClientBuilder.ClientDateOfBirth, clientFromRepo.DateOfBirth);
        }

        [Fact]
        public async Task Get_Client_By_Name()
        {
            var existingClient = ClientBuilder.Build();
            _dbContext.Clients.Add(existingClient);
            await _dbContext.SaveChangesAsync();
            
            var clientName = existingClient.Name;
            _output.WriteLine($"ClientName: { clientName }");

            var clientListFromRepo = await _repository.GetClientsByNameAsync(clientName);
            Assert.Equal(ClientBuilder.ClientName, clientListFromRepo.ToList().First().Name);
        }
    }
}