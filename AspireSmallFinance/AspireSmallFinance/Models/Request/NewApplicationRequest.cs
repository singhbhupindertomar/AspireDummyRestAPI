namespace AspireSmallFinance.Models.Request
{
    public class NewApplicationRequest
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public decimal LoanAmount { get; set; }
    }
}
