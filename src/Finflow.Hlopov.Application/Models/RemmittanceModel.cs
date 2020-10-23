using Finflow.Hlopov.Application.Models.Base;

namespace Finflow.Hlopov.Application.Models
{
    public class RemmittanceModel: GuidKeyModel
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public ClientModel Sender { get; set; }
        public ClientModel Receiver { get; set; }
        public FundModel Funds{ get; set; }
    }
}