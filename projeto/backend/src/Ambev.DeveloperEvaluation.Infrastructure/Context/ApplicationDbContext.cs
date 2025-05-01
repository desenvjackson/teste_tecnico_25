
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Sale)  
                .HasForeignKey(i => i.SaleId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SaleItem>()
                .Property(i => i.Discount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SaleItem>()
                .Property(i => i.Total)
                .HasPrecision(18, 2);
        }

        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Sales.Any())
            {
                var sales = new List<Sale>

        {
            new Sale
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                Client = "Cliente A",
                Branch = "Filial A",
                TotalValue = 45.0m,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 5,
                        UnitPrice = 10.0m,
                        Discount = 0.1m,
                        TotalValue = 45.0m
                    }
                }
            },
            new Sale
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                Client = "Cliente B",
                Branch = "Filial B",
                TotalValue = 160.0m,
                Items = new List<SaleItem>
                {
                    new SaleItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 10,
                        UnitPrice = 20.0m,
                        Discount = 0.2m,
                        TotalValue = 160.0m
                    }
                }
            }
        };

                context.Sales.AddRange(sales);
                context.SaveChanges();

               
            }
        }
    }
}
