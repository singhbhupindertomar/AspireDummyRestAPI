using AspireSmallFinance.Models.Entities;

namespace AspireSmallFinance.Models.Response
{
    public class LoanApplication
    {
        public int ApplicationId { get; set; }
        public string? ApplicantName { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? LoanStatus { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public decimal LoanAmount { get; set; }
        public int TotalInstallments { get; set; }
        public int? RemainingInstallments { get; set; }
        public decimal Balance { get; set; }
        public string? NextDueDate { get; set; }

    }
}
