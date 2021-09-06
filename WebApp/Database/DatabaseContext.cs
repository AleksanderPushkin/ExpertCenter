using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Database.Entity;
using WebApp.Models;


namespace WebApp.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Column> Columns { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Link> Links { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();   
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
           "server=Localhost;port=3306;database=ExpertCenter;Uid=root;Pwd=Bighappy123",
           new MySqlServerVersion(new Version(8, 0, 11))
       );
            }
        }
       
      
       
    }
}
