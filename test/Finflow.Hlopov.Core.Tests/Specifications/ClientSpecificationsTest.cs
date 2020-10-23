using Finflow.Hlopov.Core.Specifications;
using Finflow.Hlopov.Core.Tests.Builders;
using System.Linq;
using Xunit;

namespace Finflow.Hlopov.Core.Tests.Specifications
{
    public class ClientSpecificationsTest
    {
        private ClientBuilder ClientBuilder { get; } = new ClientBuilder();

        [Fact]
        public void Matches_Client_With_ClientName_Spec()
        {
            var spec = new ClientSpecification(ClientBuilder.ClientName1);

            var result = ClientBuilder.GetClientsCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(ClientBuilder.ClientId1, result.Id);
        }

        [Fact]
        public void Matches_Client_With_ClientId_Spec()
        {
            var spec = new ClientSpecification(ClientBuilder.ClientId2);

            var result = ClientBuilder.GetClientsCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(ClientBuilder.ClientName2, result.Name);
        }
    }
}