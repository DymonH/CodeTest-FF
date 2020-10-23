using Finflow.Hlopov.Core.Entities;
using Finflow.Hlopov.Core.Specifications.Base;
using System;

namespace Finflow.Hlopov.Core.Specifications
{
    public sealed class RemittanceSpecification : BaseSpecification<Remittance>
    {
        public RemittanceSpecification(Guid id)
            : base(r => r.Id == id)
        {
            AddIncludes();
        }

        public RemittanceSpecification() : base(null)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(f => f.ReceiveCurrency);
            AddInclude(f => f.SendCurrency);
            AddInclude(r => r.Receiver);
            AddInclude(r => r.Sender);
            AddInclude(r => r.Statuses);
            AddInclude("Statuses.RemittanceStatus");
            AddInclude("Statuses.RemittanceStatus.Status");
        }
    }
}