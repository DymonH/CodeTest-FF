using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Specifications.Base;

namespace Finflow.Hlopov.Core.Specifications
{
    public sealed class ClientSpecification : BaseSpecification<Client>
    {
        public ClientSpecification(int id)
            : base(r => r.Id == id)
        {
        }

        public ClientSpecification(string name)
            : base(r => r.Name.ToLower().Contains(name.ToLower()) || r.Surname.ToLower().Contains(name.ToLower()))
        {
        }

        public ClientSpecification()
            : base(null)
        {
        }
    }
}