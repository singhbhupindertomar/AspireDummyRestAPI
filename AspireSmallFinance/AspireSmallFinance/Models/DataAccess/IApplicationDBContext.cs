using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace AspireSmallFinance.Models.DataAccess
{
    public interface IApplicationDBContext
    {
        DbSet<Users> Users { get; set; }
        DbSet<Payments> Payments { get; set; }
        DbSet<LoanApplications> LoanApplications { get; set; }

        void SaveChangesDB();
       
    }
}
