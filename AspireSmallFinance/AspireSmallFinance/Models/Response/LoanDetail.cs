using AspireSmallFinance.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AspireSmallFinance.Models.Response
{
    public class LoanDetail : LoanApplication
    {
        public List<Payment>? Payments { get; set; }

        public LoanDetail() {
            Payments = new List<Payment>();
        }
    }
}
