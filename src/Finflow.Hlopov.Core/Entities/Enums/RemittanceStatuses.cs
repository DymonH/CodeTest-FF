using System.ComponentModel;

namespace Finflow.Hlopov.Core.Entities.Enums
{
    public enum RemittanceStatuses
    {
        [Description("Создан")]
        Created,
        [Description("Отправлен")]
        Sent,
        [Description("Возвращен")]
        Refunded,
        [Description("Выплачен")]
        Paid,
        [Description("Отменен")]
        Canceled
    }
}