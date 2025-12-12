namespace StoreDAL.Data.InitDataFactory;
using System;
using StoreDAL.Entities;

public class ReleaseDataFactory : IDataFactory
{
    public Category[] GetCategoryData()
    {
        return new[]
        {
            new Category(1, "fruits"),
            new Category(2, "water"),
            new Category(3, "vegetables"),
            new Category(4, "seafood"),
            new Category(5, "meet"),
            new Category(6, "grocery"),
            new Category(7, "milk food"),
            new Category(8, "smartphones"),
            new Category(9, "laptop"),
            new Category(10, "photocameras"),
            new Category(11, "kitchen accesories"),
            new Category(12, "spices"),
            new Category(13, "Juice"),
            new Category(14, "alcohol drinks"),
        };
    }

    public CustomerOrder[] GetCustomerOrderData()
    {
        return new[]
        {
            new CustomerOrder(1, new DateTime(2024, 1, 15, 10, 30, 0), 2, 1),
            new CustomerOrder(2, new DateTime(2024, 1, 10, 14, 20, 0), 2, 4),
            new CustomerOrder(3, new DateTime(2024, 1, 5, 9, 15, 0), 2, 6),
        };
    }

    public Manufacturer[] GetManufacturerData()
    {
        return new[]
        {
            new Manufacturer(1, "TechCorp"),
            new Manufacturer(2, "FashionBrand"),
            new Manufacturer(3, "FoodCo"),
            new Manufacturer(4, "BookPublisher"),
            new Manufacturer(5, "HomeGoods Inc"),
        };
    }

    public OrderDetail[] GetOrderDetailData()
    {
        return new[]
        {
            new OrderDetail(1, 1, 6, 3.99m, 2), // 2x Fresh Apples
            new OrderDetail(2, 1, 8, 4.99m, 1), // 1x Orange Juice
            new OrderDetail(3, 1, 9, 1.29m, 3), // 3x Mineral Water
            new OrderDetail(4, 2, 10, 12.99m, 1), // 1x Premium Coffee
            new OrderDetail(5, 2, 11, 5.99m, 1), // 1x Black Pepper
            new OrderDetail(6, 2, 12, 89.99m, 1), // 1x Knife Set
            new OrderDetail(7, 3, 1, 999.99m, 1), // 1x iPhone 15 Pro
        };
    }

    public OrderState[] GetOrderStateData()
    {
        return new[]
        {
            new OrderState(1, "New Order"),
            new OrderState(2, "Cancelled by user"),
            new OrderState(3, "Cancelled by administrator"),
            new OrderState(4, "Confirmed"),
            new OrderState(5, "Moved to delivery company"),
            new OrderState(6, "In delivery"),
            new OrderState(7, "Delivered to client"),
            new OrderState(8, "Delivery confirmed by client"),
        };
    }

    public Product[] GetProductData()
    {
        return new[]
        {
            new Product(1, 1, 1, "Latest iPhone with A17 Pro chip", 999.99m, 50), // iPhone 15 Pro - TechCorp
            new Product(2, 2, 1, "Flagship Android phone with AI features", 899.99m, 40), // Samsung Galaxy S24 - TechCorp
            new Product(3, 3, 1, "Professional laptop for creative work", 2499.99m, 20), // MacBook Pro - TechCorp
            new Product(4, 4, 1, "High-performance Windows laptop", 1799.99m, 25), // Dell XPS 15 - TechCorp
            new Product(5, 5, 1, "Professional mirrorless camera", 3899.99m, 10), // Canon EOS R5 - TechCorp
            new Product(6, 6, 3, "Fresh red apples, 1kg", 3.99m, 100), // Fresh Apples - FoodCo
            new Product(7, 7, 3, "Organic carrots, 500g", 2.49m, 80), // Organic Carrots - FoodCo
            new Product(8, 8, 3, "100% pure orange juice", 4.99m, 60), // Orange Juice - FoodCo
            new Product(9, 9, 3, "Natural spring water", 1.29m, 200), // Mineral Water - FoodCo
            new Product(10, 10, 3, "Arabica coffee beans, 500g", 12.99m, 45), // Premium Coffee - FoodCo
            new Product(11, 11, 3, "Whole black pepper, 100g", 5.99m, 70), // Black Pepper - FoodCo
            new Product(12, 12, 5, "Professional 8-piece knife set", 89.99m, 30), // Knife Set - HomeGoods Inc
        };
    }

    public ProductTitle[] GetProductTitleData()
    {
        return new[]
        {
            new ProductTitle(1, "iPhone 15 Pro", 8),
            new ProductTitle(2, "Samsung Galaxy S24", 8),
            new ProductTitle(3, "MacBook Pro 16", 9),
            new ProductTitle(4, "Dell XPS 15", 9),
            new ProductTitle(5, "Canon EOS R5", 10),
            new ProductTitle(6, "Fresh Apples", 1),
            new ProductTitle(7, "Organic Carrots", 3),
            new ProductTitle(8, "Orange Juice 1L", 13),
            new ProductTitle(9, "Mineral Water 500ml", 2),
            new ProductTitle(10, "Premium Coffee Beans", 6),
            new ProductTitle(11, "Black Pepper", 12),
            new ProductTitle(12, "Stainless Steel Knife Set", 11),
        };
    }

    public User[] GetUserData()
    {
        return new[]
        {
            new User(1, "Admin", "User", "admin", "admin123", 1), // Admin role
            new User(2, "John", "Doe", "user1", "password123", 2), // Registered role
        };
    }

    public UserRole[] GetUserRoleData()
    {
        return new[]
        {
            new UserRole(1, "Admin"),
            new UserRole(2, "Registered"),
            new UserRole(3, "Guest"),
        };
    }
}
