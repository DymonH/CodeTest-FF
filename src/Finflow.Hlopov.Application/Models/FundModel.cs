namespace Finflow.Hlopov.Application.Models
{
    public class FundModel
    {
        public decimal SendAmount { get; set; }
        public decimal ReceiveAmount { get; set; }
        public decimal Rate { get; set; }
        public int SendCurrency { get; set; }
        public int ReceiveCurrency { get; set; }
    }
}