namespace StoreDAL.Data;

using System;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;

public class StoreDbContext : DbContext
{
    private readonly IDataFactory factory;

    public StoreDbContext(DbContextOptions<StoreDbContext> options, IDataFactory factory)
        : base(options)
    {
        this.factory = factory;
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<CustomerOrder> CustomerOrders { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<OrderState> OrderStates { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductTitle> ProductTitles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.UserRole)
            .WithMany(ur => ur.Users)
            .HasForeignKey(u => u.UserRoleId)
            .IsRequired();

        modelBuilder.Entity<ProductTitle>()
            .HasOne(pt => pt.Category)
            .WithMany(c => c.ProductTitles)
            .HasForeignKey(pt => pt.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductTitle)
            .WithMany(pt => pt.Products)
            .HasForeignKey(p => p.ProductTitleId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.ManufacturerId)
            .IsRequired();

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.Customer)
            .WithMany(u => u.CustomerOrders)
            .HasForeignKey(co => co.CustomerId)
            .IsRequired();

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.OrderState)
            .WithMany(os => os.CustomerOrders)
            .HasForeignKey(co => co.OrderStateId)
            .IsRequired();

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.CustomerOrder)
            .WithMany(co => co.CustomerOrderDetails)
            .HasForeignKey(od => od.CustomerOrderId)
            .IsRequired();

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.CustomerOrderDetails)
            .HasForeignKey(od => od.ProductId)
            .IsRequired();

        modelBuilder.Entity<Category>().HasData(this.factory.GetCategoryData());
        modelBuilder.Entity<Manufacturer>().HasData(this.factory.GetManufacturerData());
        modelBuilder.Entity<OrderState>().HasData(this.factory.GetOrderStateData());
        modelBuilder.Entity<UserRole>().HasData(this.factory.GetUserRoleData());
        modelBuilder.Entity<User>().HasData(this.factory.GetUserData());
        modelBuilder.Entity<ProductTitle>().HasData(this.factory.GetProductTitleData());
        modelBuilder.Entity<Product>().HasData(this.factory.GetProductData());
        modelBuilder.Entity<CustomerOrder>().HasData(this.factory.GetCustomerOrderData());
        modelBuilder.Entity<OrderDetail>().HasData(this.factory.GetOrderDetailData());
    }
}
