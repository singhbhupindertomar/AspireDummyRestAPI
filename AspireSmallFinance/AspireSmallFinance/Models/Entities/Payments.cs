using Microsoft.VisualBasic;

namespace AspireSmallFinance.Models.Entities
{
    public class Payments
    {
        public int PaymentSysId { get; set; }
        public int LoanApplicationSysId { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PaymentDone {  get; set; }
        public bool IsSettledFlag { get; set; }
    }
}
