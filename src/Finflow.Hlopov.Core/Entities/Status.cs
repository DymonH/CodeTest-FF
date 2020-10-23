using Finflow.Hlopov.Core.Entities.Base;

namespace Finflow.Hlopov.Core.Entities
{
    public class Status : IdEntity
    {
        public string Value { get; set; }

        public static Status Create(int id, string value)
        {
            var status = new Status { Id = id, Value = value };
            return status;
        }
    }
}