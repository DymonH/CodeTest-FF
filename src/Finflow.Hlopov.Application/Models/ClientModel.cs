using Finflow.Hlopov.Application.Models.Base;
using System;

namespace Finflow.Hlopov.Application.Models
{
    public class ClientModel: IntKeyModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}