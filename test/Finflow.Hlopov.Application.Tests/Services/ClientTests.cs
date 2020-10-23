using Finflow.Hlopov.Application.Services;
using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Interfaces;
using Finflow.Hlopov.Core.Repositories;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Finflow.Hlopov.Application.Tests.Services
{
    public class ClientTests
    {
        private Mock<IClientRepository> _mockClientRepository;
        private Mock<IApplicationLogger<ClientService>> _mockAppLogger;

        public ClientTests()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockAppLogger = new Mock<IApplicationLogger<ClientService>>();
        }

        [Fact]
        public async Task Get_Client_List()
        {
            var client1 = Client.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>());
            var client2 = Client.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>());

            _mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(client1);
            _mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(client2);

            var clientService = new ClientService(_mockClientRepository.Object, _mockAppLogger.Object);
            var productList = await clientService.GetClientList();

            _mockClientRepository.Verify(x => x.GetClientListAsync(), Times.Once);
        }

        [Fact]
        public async Task Create_New_Client()
        {
            var client = Client.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>());
            Client nullClient = null;

            _mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullClient);
            _mockClientRepository.Setup(x => x.AddAsync(client)).ReturnsAsync(client);

            var clientService = new ClientService(_mockClientRepository.Object, _mockAppLogger.Object);
            var createdClientDto = await clientService.Create(new Models.ClientModel { Id = client.Id, Name = client.Name, DateOfBirth = client.DateOfBirth, Surname = client.Surname });

            _mockClientRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockClientRepository.Verify(x => x.AddAsync(client), Times.Once);
        }

        [Fact]
        public async Task Create_New_Product_Validate_If_Exist()
        {
            var client = Client.Create(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>());
            Client nullClient = null;

            _mockClientRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(nullClient);
            _mockClientRepository.Setup(x => x.AddAsync(client)).ReturnsAsync(client);

            var clientService = new ClientService(_mockClientRepository.Object, _mockAppLogger.Object);

            _mockClientRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockClientRepository.Verify(x => x.AddAsync(client), Times.Once);

            await Assert.ThrowsAsync<ApplicationException>(async () =>
                await clientService.Create(new Models.ClientModel { Id = client.Id, Name = client.Name, DateOfBirth = client.DateOfBirth, Surname = client.Surname }));
        }
    }
}