using InventoryManagement.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<SalesOfficer> SalesOfficers { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTypeDetails> ProductsTypeDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }

            modelBuilder.Entity<SalesOfficer>()
            .HasOne(u => u.ApplicationUser)
            .WithMany()  
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Supervisor>()
            .HasOne(u => u.ApplicationUser)
            .WithMany()
            .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Product>()
           .HasOne(p => p.ProductTypeName)
           .WithMany()
           .HasForeignKey(p => p.ProdType);
        }

    }
}
