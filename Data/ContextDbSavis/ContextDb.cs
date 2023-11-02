using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContextDbSavis
{
    public class ContextDb : DbContext
    {   public ContextDb()
        {
        }
        public ContextDb(DbContextOptions options) : base(options)
        {
        }

     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DUNGNGUYEN\SQLEXPRESS;Initial Catalog=Savis_Project_Alone;Integrated Security=True");
        }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductDetail>  ProductDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }      
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails>  CartDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Notifi>  Notifis { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Vorcher>  Vorchers { get; set; }
        public DbSet<VorcherDetail>  VorcherDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
