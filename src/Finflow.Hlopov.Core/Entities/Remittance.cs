using Finflow.Hlopov.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace Finflow.Hlopov.Core.Entities
{
    public class Remittance: GuidEntity
    {
        public string Code { get; set; }
        public Client Sender { get; set; }
        public Client Receiver { get; set; }
        public decimal SendAmount { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal Rate { get; set; }
        public Currency SendCurrency { get; set; }
        public Currency ReceiveCurrency { get; set; }
        public ICollection<RemittanceStatuses> Statuses { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int SendCurrencyId { get; set; }
        public int ReceiveCurrencyId { get; set; }
    }
}