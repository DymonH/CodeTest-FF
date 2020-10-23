using Finflow.Hlopov.Core.Entities.Base;
using System;

namespace Finflow.Hlopov.Core.Entities
{
    public class Client: IdEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public static Client Create(int id, string name, string surname, DateTime dateOfBirth)
        {
            var client = new Client
            {
                Id = id,
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth
            };

            return client;
        }
    }
}