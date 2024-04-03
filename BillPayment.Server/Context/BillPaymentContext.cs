using BillPayment.Server.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BillPayment.Server.Context
{
    public class BillPaymentContext : DbContext
    {
        public BillPaymentContext(DbContextOptions<BillPaymentContext> options) : base(options) 
        {
            
        }

        

        public DbSet<AccountPayable> AccountsPayable { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountPayable>().HasData(
                new AccountPayable()
                {
                    Id = 1,
                    Name = "Light",
                    DueDate = DateTime.Today.AddDays(3),
                    PaymentDate = DateTime.Today,
                    OriginalAmount = 100,
                    TotalAmount = 100,
                },
                new AccountPayable()
                {
                    Id = 2,
                    Name = "Water",
                    DueDate = DateTime.Today.AddDays(-2),
                    PaymentDate = DateTime.Today.AddDays(1),
                    LateDays = 3,
                    OriginalAmount = 50,
                    FineAmount = 1,
                    FinePercentage = 2,
                    InterestAmount = 0.15M,
                    InterestPercentage = 0.1M,
                    TotalAmount = 51.15M,
                });
        }
    }
}
