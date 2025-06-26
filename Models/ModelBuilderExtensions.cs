using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.ViewModels
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOfficer>().HasData(
                new SalesOfficer() 
                {
                    Id = 1,
                    Name = "Mary",
                    Email = "mary@pragim",
                    CNIC = "12345-6789123-1",
                    Gender = GenderType.Female,
                    Address = "LA" 
                },
                new SalesOfficer() 
                { 
                    Id = 2, 
                    Name = "John", 
                    Email = "john@pragim", 
                    CNIC = "12345-6789123-2", 
                    Gender = GenderType.Male, 
                    Address = "ViceCity" 
                },
                new SalesOfficer() 
                {
                    Id = 3,
                    Name = "Sam",
                    Email = "sam@pragim",
                    CNIC = "12345-6789123-3",
                    Gender = GenderType.Male,
                    Address = "LibertyCity"
                }

                );
            modelBuilder.Entity<Supervisor>().HasData(
                 new Supervisor() 
                 {
                     Id = 1,
                     Name = "Sarah",
                     Email = "sarah@pragim",
                     CNIC = "12345-6789123-4",
                     Gender = GenderType.Female, 
                     Address = "GrooveStreet" 
                 },
                new Supervisor() 
                {
                    Id = 2, 
                    Name = "James",
                    Email = "james@pragim",
                    CNIC = "12345-6789123-5",
                    Gender = GenderType.Male,
                    Address = "ViceCity" 
                },
                new Supervisor() 
                { 
                    Id = 3,
                    Name = "Peter",
                    Email = "peter@pragim",
                    CNIC = "12345-6789123-6", 
                    Gender = GenderType.Male,
                    Address = "DownTown" 
                }

                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    ProductName = "Mouse",
                    ProdType = ProductType.Hardware,
                    Description = "This is a black mouse for pc",
                    PurchasedPrice = 100.01,
                    SalesPrice = 120.22,
                    CreatedBy = "Jack",
                    CreatedDate = System.DateTime.Now,
                    ModifiedBy = "Jack",
                    ModifiedDate = System.DateTime.Now,
                    Quantity = 20,
                    Status = true
                },
                 new Product
                 {
                     Id = 2,
                     ProductName = "RAM",
                     ProdType = ProductType.Electric,
                     Description = "This is a 8gb ram for pc",
                     PurchasedPrice = 1000.01,
                     SalesPrice = 1200.22,
                     CreatedBy = "Jack",
                     CreatedDate = System.DateTime.Now,
                     ModifiedBy = "Jack",
                     ModifiedDate = System.DateTime.Now,
                     Quantity = 10,
                     Status = true
                 },
                  new Product
                  {
                      Id = 3,
                      ProductName = "watch",
                      ProdType = ProductType.Mechanical,
                      Description = "This is a black watch to wear",
                      PurchasedPrice = 1050.01,
                      SalesPrice = 1520.22,
                      CreatedBy = "Jack",
                      CreatedDate = System.DateTime.Now,
                      ModifiedBy = "Jack",
                      ModifiedDate = System.DateTime.Now,
                      Quantity = 30,
                      Status = true
                  }
                );
            modelBuilder.Entity<ProductTypeDetails>().HasData(
                 new ProductTypeDetails
                 {
                     Id = 1,
                     ProductTypeName = ProductType.Electric,
                     Status = true
                 },
                 new ProductTypeDetails
                 {
                     Id = 2,
                     ProductTypeName = ProductType.Hardware,
                     Status = true
                 },
                 new ProductTypeDetails
                 {
                     Id = 3,
                     ProductTypeName = ProductType.Mechanical,
                     Status = true
                 }

                );
        }
    }
}
