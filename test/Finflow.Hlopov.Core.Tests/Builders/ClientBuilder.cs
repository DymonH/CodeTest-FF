using Finflow.Hlopov.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Finflow.Hlopov.Core.Tests.Builders
{
    public class ClientBuilder
    {
        public int ClientId1 => 1;
        public int ClientId2 => 2;
        public string ClientName1 => "Robert";
        public string ClientName2 => "Martin";

        public List<Client> GetClientsCollection()
        {
            return new List<Client>()
            {
                Client.Create(1, "Robert", "Martin", DateTime.ParseExact("12/05/1952", "MM/dd/yyyy", CultureInfo.InvariantCulture)),
                Client.Create(2, "Martin", "Fowler", DateTime.ParseExact("01/01/1963", "MM/dd/yyyy", CultureInfo.InvariantCulture))
            };
        }
    }
}