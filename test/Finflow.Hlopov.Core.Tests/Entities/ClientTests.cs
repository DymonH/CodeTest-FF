using Finflow.Hlopov.Core.Entities;
using System;
using Xunit;

namespace Finflow.Hlopov.Core.Tests.Entities
{
    public class ClientTests
    {
        private int _testClientId = 1;
        private string _testClientName = "Robert";
        private string _testClientSurname = "Martin";
        private DateTime _testClientDOB = DateTime.ParseExact("12/05/1952", "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        [Fact]
        public void Create_Client()
        {
            var client = Client.Create(_testClientId, _testClientName, _testClientSurname, _testClientDOB);

            Assert.Equal(_testClientId, client.Id);
            Assert.Equal(_testClientName, client.Name);
            Assert.Equal(_testClientSurname, client.Surname);
            Assert.Equal(_testClientDOB, client.DateOfBirth);
        }
    }
}