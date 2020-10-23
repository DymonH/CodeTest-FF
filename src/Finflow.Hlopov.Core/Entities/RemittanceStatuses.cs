using System;

namespace Finflow.Hlopov.Core.Entities
{
    public class RemittanceStatuses
    {
        public Guid RemittanceId { get; set; }

        public int RemittanceStatusId { get; set; }

        public Remittance Remittance { get; set; }

        public RemittanceStatus RemittanceStatus { get; set; }
    }
}