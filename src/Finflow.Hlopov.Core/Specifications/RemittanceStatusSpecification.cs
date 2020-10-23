using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Specifications.Base;

namespace Finflow.Hlopov.Core.Specifications
{
    public sealed class RemittanceStatusSpecification : BaseSpecification<RemittanceStatus>
    {
        public RemittanceStatusSpecification(int id)
            : base(r => r.Id == id)
        {
            AddInclude(r => r.Status);
        }
    }
}