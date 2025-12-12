namespace StoreDAL.Data;

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;

/// <summary>
/// Service for seeding the database with initial data.
/// </summary>
public class DatabaseSeeder
{
    private readonly StoreDbContext context;
    private readonly IDataFactory dataFactory;

    public DatabaseSeeder(StoreDbContext context, IDataFactory dataFactory)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.dataFactory = dataFactory ?? throw new ArgumentNullException(nameof(dataFactory));
    }

    /// <summary>
    /// Seeds the database with initial data if it doesn't already exist.
    /// </summary>
    public void Seed()
    {
        try
        {
            // Clear the change tracker to avoid conflicts with entities seeded via HasData() in migrations
            this.context.ChangeTracker.Clear();

            this.SeedUserRoles();

            this.SeedOrderStates();

            this.SeedCategories();

            this.SeedManufacturers();

            this.SeedUsers();

            this.SeedProductTitles();

            this.SeedProducts();

            // Seed orders after users and order states are seeded
            this.SeedCustomerOrders();

            // Seed order details after orders and products are seeded
            this.SeedOrderDetails();

            this.context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred while seeding the database: {ex.Message}", ex);
        }
    }

    private void SeedUserRoles()
    {
        if (!this.context.UserRoles.AsNoTracking().Any())
        {
            var userRoles = this.dataFactory.GetUserRoleData();
            if (userRoles.Length > 0)
            {
                this.context.UserRoles.AddRange(userRoles);
                Console.WriteLine($"Seeded {userRoles.Length} user roles.");
            }
        }
    }

    private void SeedOrderStates()
    {
        if (!this.context.OrderStates.AsNoTracking().Any())
        {
            var orderStates = this.dataFactory.GetOrderStateData();
            if (orderStates.Length > 0)
            {
                this.context.OrderStates.AddRange(orderStates);
                Console.WriteLine($"Seeded {orderStates.Length} order states.");
            }
        }
    }

    private void SeedCategories()
    {
        if (!this.context.Categories.AsNoTracking().Any())
        {
            var categories = this.dataFactory.GetCategoryData();
            if (categories.Length > 0)
            {
                this.context.Categories.AddRange(categories);
                Console.WriteLine($"Seeded {categories.Length} categories.");
            }
        }
    }

    private void SeedManufacturers()
    {
        if (!this.context.Manufacturers.AsNoTracking().Any())
        {
            var manufacturers = this.dataFactory.GetManufacturerData();
            if (manufacturers.Length > 0)
            {
                this.context.Manufacturers.AddRange(manufacturers);
                Console.WriteLine($"Seeded {manufacturers.Length} manufacturers.");
            }
        }
    }

    private void SeedUsers()
    {
        // Use AsNoTracking to check if users exist without tracking them
        if (!this.context.Users.AsNoTracking().Any())
        {
            var users = this.dataFactory.GetUserData();
            if (users.Length > 0)
            {
                this.context.Users.AddRange(users);
                Console.WriteLine($"Seeded {users.Length} users.");
            }
        }
    }

    private void SeedProductTitles()
    {
        if (!this.context.ProductTitles.AsNoTracking().Any())
        {
            var productTitles = this.dataFactory.GetProductTitleData();
            if (productTitles.Length > 0)
            {
                this.context.ProductTitles.AddRange(productTitles);
                Console.WriteLine($"Seeded {productTitles.Length} product titles.");
            }
        }
    }

    private void SeedProducts()
    {
        if (!this.context.Products.AsNoTracking().Any())
        {
            var products = this.dataFactory.GetProductData();
            if (products.Length > 0)
            {
                this.context.Products.AddRange(products);
                Console.WriteLine($"Seeded {products.Length} products.");
            }
        }
    }

    private void SeedCustomerOrders()
    {
        if (!this.context.CustomerOrders.AsNoTracking().Any())
        {
            var customerOrders = this.dataFactory.GetCustomerOrderData();
            if (customerOrders.Length > 0)
            {
                this.context.CustomerOrders.AddRange(customerOrders);
                Console.WriteLine($"Seeded {customerOrders.Length} customer orders.");
            }
        }
    }

    private void SeedOrderDetails()
    {
        if (!this.context.OrderDetails.AsNoTracking().Any())
        {
            var orderDetails = this.dataFactory.GetOrderDetailData();
            if (orderDetails.Length > 0)
            {
                this.context.OrderDetails.AddRange(orderDetails);
                Console.WriteLine($"Seeded {orderDetails.Length} order details.");
            }
        }
    }
}

