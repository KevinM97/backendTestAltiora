using BackTest.Domain;
using BackTest.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Infrastructure.Persistence
{
    public class AltioraDbContext : IdentityDbContext<User>
    {
        public AltioraDbContext(DbContextOptions<AltioraDbContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userName = "AltioraAudit";
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userName;
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = userName;
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(x=>x.Id).HasMaxLength(36); //250
            builder.Entity<User>().Property(x=>x.NormalizedUserName).HasMaxLength(90);
            builder.Entity<IdentityRole>().Property(x=>x.Id).HasMaxLength(36);
            builder.Entity<IdentityRole>().Property(x=>x.NormalizedName).HasMaxLength(90);
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem>? OrdersItems { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        

    }
}
