namespace StoreDAL.Data;

using System;
using System.Collections.Generic;
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
            this.SaveChangesWithLog("user roles");

            this.SeedOrderStates();
            this.SaveChangesWithLog("order states");

            this.SeedCategories();
            this.SaveChangesWithLog("categories");

            this.SeedManufacturers();
            this.SaveChangesWithLog("manufacturers");

            this.SeedUsers();
            this.SaveChangesWithLog("users");

            this.SeedProductTitles();
            this.SaveChangesWithLog("product titles");

            this.SeedProducts();
            this.SaveChangesWithLog("products");

            // Seed orders after users and order states are seeded
            this.SeedCustomerOrders();
            this.SaveChangesWithLog("customer orders");

            // Seed order details after orders and products are seeded
            this.SeedOrderDetails();
            this.SaveChangesWithLog("order details");
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;
            throw new InvalidOperationException($"An error occurred while seeding the database: {errorMessage}", ex);
        }
    }

    private void SeedUserRoles()
    {
        var userRoles = this.dataFactory.GetUserRoleData();
        this.SeedEntities(this.context.UserRoles, userRoles, "user roles");
    }

    private void SeedOrderStates()
    {
        var orderStates = this.dataFactory.GetOrderStateData();
        this.SeedEntities(this.context.OrderStates, orderStates, "order states");
    }

    private void SeedCategories()
    {
        var categories = this.dataFactory.GetCategoryData();
        this.SeedEntities(this.context.Categories, categories, "categories");
    }

    private void SeedManufacturers()
    {
        var manufacturers = this.dataFactory.GetManufacturerData();
        this.SeedEntities(this.context.Manufacturers, manufacturers, "manufacturers");
    }

    private void SeedUsers()
    {
        var users = this.dataFactory.GetUserData();
        this.SeedEntities(this.context.Users, users, "users");
    }

    private void SeedProductTitles()
    {
        var productTitles = this.dataFactory.GetProductTitleData();
        this.SeedEntities(this.context.ProductTitles, productTitles, "product titles");
    }

    private void SeedProducts()
    {
        var products = this.dataFactory.GetProductData();
        this.SeedEntities(this.context.Products, products, "products");
    }

    private void SeedCustomerOrders()
    {
        var customerOrders = this.dataFactory.GetCustomerOrderData();
        this.SeedEntities(this.context.CustomerOrders, customerOrders, "customer orders");
    }

    private void SeedOrderDetails()
    {
        var orderDetails = this.dataFactory.GetOrderDetailData();
        this.SeedEntities(this.context.OrderDetails, orderDetails, "order details");
    }

    private void SeedEntities<T>(DbSet<T> dbSet, IEnumerable<T> entities, string entityName)
        where T : BaseEntity
    {
        var existingIds = dbSet.AsNoTracking()
            .Select(entity => entity.Id)
            .ToHashSet();

        var missingEntities = entities
            .Where(entity => !existingIds.Contains(entity.Id))
            .ToArray();

        if (missingEntities.Length == 0)
        {
            return;
        }

        dbSet.AddRange(missingEntities);
        Console.WriteLine($"Seeded {missingEntities.Length} {entityName}.");
    }

    /// <summary>
    /// Saves changes and surfaces which stage failed.
    /// </summary>
    /// <param name="stage">Description of what was just seeded.</param>
    private void SaveChangesWithLog(string stage)
    {
        try
        {
            this.context.SaveChanges();
        }
        catch (Exception ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;
            throw new InvalidOperationException($"Failed saving {stage}: {errorMessage}", ex);
        }
    }
}

