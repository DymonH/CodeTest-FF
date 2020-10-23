using Finflow.Hlopov.Core.Entities;
using System;
using System.Globalization;

namespace Finflow.Hlopov.Infrastructure.Tests.Builders
{
    public class ClientBuilder
    {
        public int ClientId => 1;
        public string ClientName => "Robert";
        public string ClientSurname => "Martin";
        public DateTime ClientDateOfBirth => DateTime.ParseExact("12/05/1952", "MM/dd/yyyy", CultureInfo.InvariantCulture);
        
        public Client Build()
        {
            return Client.Create(ClientId, ClientName, ClientSurname, ClientDateOfBirth);
        }
    }
}