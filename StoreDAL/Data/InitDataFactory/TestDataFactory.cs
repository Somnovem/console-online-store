using System.Security.Cryptography;

namespace StoreDAL.Data.InitDataFactory;

using Entities;

public class TestDataFactory : AbstractDataFactory
{
    public override Category[] GetCategoryData()
    {
        return new[]
        {
            new Category(1, "fruits"),
            new Category(2, "water"),
            new Category(3, "vegetables"),
            new Category(4, "seafood"),
            new Category(5, "meat"),
            new Category(6, "grocery"),
            new Category(7, "milk food"),
            new Category(8, "smartphones"),
            new Category(9, "laptop"),
            new Category(10, "photocameras"),
            new Category(11, "kitchen accessories"),
            new Category(12, "spices"),
            new Category(13, "juice"),
            new Category(14, "alcohol drinks"),
        };
    }

    public override CustomerOrder[] GetCustomerOrderData()
    {
        return new[]
        {
            new CustomerOrder(1, "2024-01-15 10:30:00", 2, 8), // Alice - Delivered and confirmed
            new CustomerOrder(2, "2024-01-20 14:15:00", 3, 7), // Bob - Delivered to client
            new CustomerOrder(3, "2024-02-01 09:45:00", 4, 6), // Sarah - In delivery
            new CustomerOrder(4, "2024-02-05 16:20:00", 5, 5), // Mike - Moved to delivery company
            new CustomerOrder(5, "2024-02-10 11:00:00", 6, 4), // Emily - Confirmed
            new CustomerOrder(6, "2024-02-12 13:30:00", 2, 2), // Alice - Cancelled by user
            new CustomerOrder(7, "2024-02-15 15:45:00", 3, 1), // Bob - New Order
            new CustomerOrder(8, "2024-02-18 08:15:00", 4, 1), // Sarah - New Order
        };
    }

    public override Manufacturer[] GetManufacturerData()
    {
        return new[]
        {
            new Manufacturer(1, "Apple Inc."),
            new Manufacturer(2, "Samsung"),
            new Manufacturer(3, "Dell"),
            new Manufacturer(4, "HP"),
            new Manufacturer(5, "Canon"),
            new Manufacturer(6, "Nikon"),
            new Manufacturer(7, "Coca-Cola"),
            new Manufacturer(8, "PepsiCo"),
            new Manufacturer(9, "Nestle"),
            new Manufacturer(10, "Danone"),
            new Manufacturer(11, "Fresh Farms"),
            new Manufacturer(12, "Ocean Catch"),
            new Manufacturer(13, "Spice Masters"),
            new Manufacturer(14, "KitchenPro"),
        };
    }

    public override OrderDetail[] GetOrderDetailData()
    {
        return new[]
        {
            // Order 1 (Alice - Delivered and confirmed)
            new OrderDetail(1, 1, 1, 2.99m, 5), // 5 Apples
            new OrderDetail(2, 1, 8, 3.99m, 2), // 2 Tomatoes
            new OrderDetail(3, 1, 16, 3.49m, 1), // 1 Milk

            // Order 2 (Bob - Delivered to client)
            new OrderDetail(4, 2, 19, 999.99m, 1), // 1 iPhone
            new OrderDetail(5, 2, 24, 89.99m, 1), // 1 Chef Knife

            // Order 3 (Sarah - In delivery)
            new OrderDetail(6, 3, 11, 18.99m, 2), // 2 Salmon fillets
            new OrderDetail(7, 3, 4, 1.99m, 3), // 3 Water bottles
            new OrderDetail(8, 3, 26, 8.99m, 1), // 1 Black Pepper

            // Order 4 (Mike - Moved to delivery company)
            new OrderDetail(9, 4, 21, 1199.99m, 1), // 1 MacBook

            // Order 5 (Emily - Confirmed)
            new OrderDetail(10, 5, 2, 1.49m, 6), // 6 Bananas
            new OrderDetail(11, 5, 9, 2.29m, 3), // 3 Carrots
            new OrderDetail(12, 5, 17, 5.99m, 2), // 2 Greek Yogurts

            // Order 6 (Alice - Cancelled by user)
            new OrderDetail(13, 6, 28, 24.99m, 2), // 2 Wine bottles

            // Order 7 (Bob - New Order)
            new OrderDetail(14, 7, 23, 1499.99m, 1), // 1 Camera
            new OrderDetail(15, 7, 25, 34.99m, 1), // 1 Cutting Board

            // Order 8 (Sarah - New Order)
            new OrderDetail(16, 8, 14, 8.99m, 3), // 3 Chicken Breasts
            new OrderDetail(17, 8, 10, 1.99m, 2), // 2 Lettuce
            new OrderDetail(18, 8, 6, 4.99m, 1), // 1 Orange Juice
        };
    }

    public override OrderState[] GetOrderStateData()
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

    public override Product[] GetProductData()
    {
        return new[]
        {
            // Fruits
            new Product(1, 1, 11, "Fresh red apples from local orchards", 2.99m),
            new Product(2, 2, 11, "Ripe yellow bananas", 1.49m),
            new Product(3, 3, 11, "Juicy oranges perfect for breakfast", 3.49m),

            // Beverages
            new Product(4, 4, 7, "Pure mountain spring water", 1.99m),
            new Product(5, 5, 7, "Refreshing sparkling water with minerals", 2.49m),
            new Product(6, 6, 8, "100% pure orange juice", 4.99m),
            new Product(7, 7, 8, "Fresh apple juice", 4.79m),

            // Vegetables
            new Product(8, 8, 11, "Fresh organic tomatoes", 3.99m),
            new Product(9, 9, 11, "Crispy orange carrots", 2.29m),
            new Product(10, 10, 11, "Fresh green lettuce", 1.99m),

            // Seafood
            new Product(11, 11, 12, "Fresh Atlantic salmon fillet", 18.99m),
            new Product(12, 12, 12, "Yellowfin tuna steaks", 22.99m),
            new Product(13, 13, 12, "Large tiger shrimp", 16.99m),

            // Meat
            new Product(14, 14, 11, "Boneless chicken breast", 8.99m),
            new Product(15, 15, 11, "Fresh ground beef 80/20", 6.99m),

            // Dairy
            new Product(16, 16, 10, "Fresh whole milk", 3.49m),
            new Product(17, 17, 10, "Creamy Greek yogurt", 5.99m),
            new Product(18, 18, 10, "Aged cheddar cheese", 7.99m),

            // Electronics
            new Product(19, 19, 1, "iPhone 15 Pro 128GB", 999.99m),
            new Product(20, 20, 2, "Galaxy S24 256GB", 849.99m),
            new Product(21, 21, 1, "MacBook Air M3", 1199.99m),
            new Product(22, 22, 3, "Dell XPS 13 Laptop", 1099.99m),
            new Product(23, 23, 5, "Canon EOS R8 Camera", 1499.99m),

            // Kitchen & Spices
            new Product(24, 24, 14, "Professional chef knife 8-inch", 89.99m),
            new Product(25, 25, 14, "Bamboo cutting board large", 34.99m),
            new Product(26, 26, 13, "Premium black peppercorns", 8.99m),
            new Product(27, 27, 13, "Himalayan sea salt", 12.99m),

            // Alcohol
            new Product(28, 28, 9, "Cabernet Sauvignon 2020", 24.99m),
            new Product(29, 29, 7, "Craft beer variety pack", 16.99m),
        };
    }

    public override ProductTitle[] GetProductTitleData()
    {
        return new[]
        {
            // Fruits
            new ProductTitle(1, "Apple", 1),
            new ProductTitle(2, "Banana", 1),
            new ProductTitle(3, "Orange", 1),

            // Water & Beverages
            new ProductTitle(4, "Mineral Water", 2),
            new ProductTitle(5, "Sparkling Water", 2),
            new ProductTitle(6, "Orange Juice", 13),
            new ProductTitle(7, "Apple Juice", 13),

            // Vegetables
            new ProductTitle(8, "Tomato", 3),
            new ProductTitle(9, "Carrot", 3),
            new ProductTitle(10, "Lettuce", 3),

            // Seafood
            new ProductTitle(11, "Salmon", 4),
            new ProductTitle(12, "Tuna", 4),
            new ProductTitle(13, "Shrimp", 4),

            // Meat
            new ProductTitle(14, "Chicken Breast", 5),
            new ProductTitle(15, "Ground Beef", 5),

            // Dairy
            new ProductTitle(16, "Whole Milk", 7),
            new ProductTitle(17, "Greek Yogurt", 7),
            new ProductTitle(18, "Cheddar Cheese", 7),

            // Electronics
            new ProductTitle(19, "iPhone", 8),
            new ProductTitle(20, "Galaxy Phone", 8),
            new ProductTitle(21, "MacBook", 9),
            new ProductTitle(22, "Dell Laptop", 9),
            new ProductTitle(23, "DSLR Camera", 10),

            // Kitchen & Spices
            new ProductTitle(24, "Chef Knife", 11),
            new ProductTitle(25, "Cutting Board", 11),
            new ProductTitle(26, "Black Pepper", 12),
            new ProductTitle(27, "Sea Salt", 12),

            // Alcohol
            new ProductTitle(28, "Red Wine", 14),
            new ProductTitle(29, "Beer", 14),
        };
    }

    public override User[] GetUserData()
    {
        return new[]
        {
            new User(1, "John", "Smith", "admin", HashPassword("admin123"), 1),
            new User(2, "Alice", "Johnson", "alice.j", HashPassword("password123"), 2),
            new User(3, "Bob", "Wilson", "bob.w", HashPassword("bobpass"), 2),
            new User(4, "Sarah", "Davis", "sarah.d", HashPassword("sarahpass"), 2),
            new User(5, "Mike", "Brown", "mike.b", HashPassword("mikepass"), 2),
            new User(6, "Emily", "Taylor", "emily.t", HashPassword("emilypass"), 2),
            new User(7, "Guest", "User", "guest", HashPassword("guest"), 3),
        };
    }

    public override UserRole[] GetUserRoleData()
    {
        return new[]
        {
            new UserRole(1, "Admin"),
            new UserRole(2, "Registered"),
            new UserRole(3, "Guest"),
        };
    }

    public static string HashPassword(string password)
    {
        // Generate random salt
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // Hash password with salt
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);

        // Combine salt + hash and return as base64
        return Convert.ToBase64String(salt.Concat(hash).ToArray());
    }
}