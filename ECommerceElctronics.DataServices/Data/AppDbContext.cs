using ECommerceElctronics.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceElctronics.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Uesrs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
                entity.HasOne(b => b.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(b => b.BrandId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Brands_Product")
                );

            modelBuilder.Entity<Product>(entity =>
               entity.HasOne(b => b.Category)
                   .WithMany(p => p.Products)
                   .HasForeignKey(b => b.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .HasConstraintName("FK_Categories_Product")
               );


            modelBuilder.Entity<Brand>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Cart>()
                .HasOne(u => u.User)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>(entity =>
                entity.HasOne(b => b.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                );

            //modelBuilder.Entity<Order>()
            //    .HasOne(u => u.Cart)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(c => c.CartId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
            .HasIndex(u => u.UserId)
            .IsUnique(false);

            modelBuilder.Entity<Cart>()
           .HasIndex(u => u.UserId)
           .IsUnique(false);


        }
    }
}
