using Microsoft.VisualBasic;

namespace AspireSmallFinance.Models.Response
{
    public class Payment
    {
        public int PaymentSysId { get; set; }
        public string? DueDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PaymentDone {  get; set; }
        public bool IsSettledFlag { get; set; }
    }
}
