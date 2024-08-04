using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireSmallFinance.Models.Entities
{
    public class LoanApplications
    {
        public int LoanApplicationSysId { get; set; }
        public int UserSysId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal LoanAmount { get; set; }
        public bool IsApprovedFlag { get; set; }
        public bool IsClosedFlag { get; set; }
    }
}
