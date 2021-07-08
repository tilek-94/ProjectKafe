using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using KafeProject.Models;
namespace KafeProject.Date
{
    public  class ApplicationContext : DbContext
    {
       public DbSet<Food> Foods { get; set; }
       public DbSet<Waiter> Waiters { get; set; }
       public DbSet<Location> Locations { get; set; }
       public DbSet<Table> Tables { get; set; }
       public DbSet<Product> Products { get; set; }
       public DbSet<ReceiptGoods> ReceiptGoods { get; set; }
       public DbSet<Recipe> Recipes { get; set; }
       public DbSet<Check> Checks { get; set; }
       public DbSet<Order> Orders { get; set; }
       public DbSet<Options> Options { get; set; }
      
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=192.168.0.113;user=kafe;password=1;database=BasaKafe;",
                 new MySqlServerVersion(new Version(5, 7, 29))
             );
        } 
    }
}
