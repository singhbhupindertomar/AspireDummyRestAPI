using System.ComponentModel;

namespace AspireSmallFinance.Constants
{
    public enum EnStatus
    {
        [Description("Pending")]
        STATUS_PENDING = 1,
        [Description("Approved")]
        STATUS_APPROVED = 2,
        [Description("Rejected")]
        STATUS_REJECTED = 3,
        [Description("Paid")]
        STATUS_PAID = 4,
        [Description("Closed")]
        STATUS_CLOSED = 5,
        [Description("Running")]
        STATUS_RUNNING = 6,

    }
}
