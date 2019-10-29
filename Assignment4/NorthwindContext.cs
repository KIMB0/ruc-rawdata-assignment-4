using System;
using Assignment4.Tests;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Category> CategoriesSet { get; set; }
        public DbSet<Product> ProductsSet { get; set; }
        public DbSet<Order> OrdersSet { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "host=localhost;db=northwind;uid=postgres;pwd=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CATEGORIES
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().Property(m => m.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(m => m.Name).HasColumnName("categoryname");
            modelBuilder.Entity<Category>().Property(m => m.Description).HasColumnName("description");

            //PRODUCTS
            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Product>().Property(m => m.Id).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(m => m.Name).HasColumnName("productname");
            modelBuilder.Entity<Product>().Property(m => m.SupplierId).HasColumnName("supplierid");
            modelBuilder.Entity<Product>().Property(m => m.CategoryId).HasColumnName("categoryid");
            modelBuilder.Entity<Product>().Property(m => m.QuantityPerUnit).HasColumnName("quantityperunit");
            modelBuilder.Entity<Product>().Property(m => m.UnitPrice).HasColumnName("unitprice");
            modelBuilder.Entity<Product>().Property(m => m.UnitsInStock).HasColumnName("unitsinstock");

            //ORDERS
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>().Property(o => o.Id).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(o => o.ShipName).HasColumnName("shipname");
            modelBuilder.Entity<Order>().Property(o => o.ShipCity).HasColumnName("shipcity");
            modelBuilder.Entity<Order>().Property(o => o.RequiredDate).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasColumnName("orderdate");

            //ORDER DETAILS
            modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
            modelBuilder.Entity<OrderDetails>().HasKey(o => new { o.OrderId, o.ProductId });
            modelBuilder.Entity<OrderDetails>().Property(o => o.OrderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetails>().Property(o => o.Discount).HasColumnName("discount");
            modelBuilder.Entity<OrderDetails>().Property(o => o.ProductId).HasColumnName("productid");
            modelBuilder.Entity<OrderDetails>().Property(o => o.Quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetails>().Property(o => o.UnitPrice).HasColumnName("unitprice");
        }
    }
}
