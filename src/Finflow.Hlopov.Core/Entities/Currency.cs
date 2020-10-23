using Finflow.Hlopov.Core.Entities.Base;

namespace Finflow.Hlopov.Core.Entities
{
    public class Currency: IdEntity
    {
        public string Value { get; set; }

        public static Currency Create(int id, string value)
        {
            var currency = new Currency
            {
                Id = id,
                Value = value
            };

            return currency;
        }
    }
}