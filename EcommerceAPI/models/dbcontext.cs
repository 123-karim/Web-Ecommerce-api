using Microsoft.EntityFrameworkCore;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EcommerceAPI.models
{
    public class dbContext : IdentityDbContext<ApplicationUser>
    {
        public dbContext(DbContextOptions<dbContext>options):base(options) 
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=\\.\pipe\MSSQL$KARIM\sql\query;Database=EcommerceData;Trusted_Connection=True;Encrypt=False");
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CartProduct>().HasOne(p => p.Cart).WithMany(p => p.cartproduct).HasForeignKey(p => p.CartId);
        //}
       


        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<CartProduct> cartProduct { get; set; }
    }
}
