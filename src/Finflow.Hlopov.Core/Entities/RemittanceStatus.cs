using Finflow.Hlopov.Core.Entities.Base;
using System;

namespace Finflow.Hlopov.Core.Entities
{
    public class RemittanceStatus: IdEntity
    {
        public DateTime StatusDate { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}