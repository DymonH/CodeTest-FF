using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Finflow.Hlopov.Web.Tests.Controllers
{
    public class ClientControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }

        public ClientControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Client_List_Test()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/Client");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Robert", stringResponse);
        }
    }
}