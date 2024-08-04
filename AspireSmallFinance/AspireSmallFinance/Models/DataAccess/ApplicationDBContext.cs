using AspireSmallFinance.Models.Entities;
using AspireSmallFinance.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace AspireSmallFinance.Models.DataAccess
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<LoanApplications> LoanApplications { get; set; }

        public ApplicationDBContext() : base(){ }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users").HasKey(key => key.UserSysId);
            modelBuilder.Entity<Payments>().ToTable("Payments").HasKey(key => key.PaymentSysId);
            modelBuilder.Entity<LoanApplications>().ToTable("LoanApplications").HasKey(key => key.LoanApplicationSysId);
        }

        public void SaveChangesDB()
        {
            base.SaveChanges();
        }
    }
}
