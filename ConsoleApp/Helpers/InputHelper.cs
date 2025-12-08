namespace ConsoleApp.Helpers;

using System;
using System.Globalization;
using StoreBLL.Models;

public static class InputHelper
{
    public static int ReadInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write($"Enter {prompt}: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
    }

    public static decimal ReadDecimal(string prompt)
    {
        decimal value;
        while (true)
        {
            Console.Write($"Enter {prompt}: ");
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number.");
            }
        }
    }

    public static string ReadString(string prompt)
    {
        string? value;
        while (true)
        {
            Console.Write($"Enter {prompt}: ");
            value = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
            else
            {
                Console.WriteLine("Input cannot be empty. Please enter a valid string.");
            }
        }
    }

    public static void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public static CategoryModel ReadCategoryModel()
    {
        Console.WriteLine("\n--- Enter Category Details ---");
        int id = ReadInt("Category ID (0 for new)");
        string name = ReadString("Category Name");
        return new CategoryModel(id, name);
    }

    public static ManufacturerModel ReadManufacturerModel()
    {
        Console.WriteLine("\n--- Enter Manufacturer Details ---");
        int id = ReadInt("Manufacturer ID (0 for new)");
        string name = ReadString("Manufacturer Name");
        return new ManufacturerModel(id, name);
    }

    public static OrderStateModel ReadOrderStateModel()
    {
        var id = ReadInt("State Id");
        var name = ReadString("State Name");
        return new OrderStateModel(id, name);
    }

    public static UserRoleModel ReadUserRoleModel()
    {
        var id = ReadInt("User Role Id");
        var name = ReadString("User Role Name");
        return new UserRoleModel(id, name);
    }

    public static ProductTitleModel ReadProductTitleModel()
    {
        Console.WriteLine("\n--- Enter Product Title Details ---");
        int id = ReadInt("Product Title ID (0 for new)");
        string title = ReadString("Product Title Name");
        int categoryId = ReadInt("Category ID");

        return new ProductTitleModel(id, title, categoryId);
    }

    public static ProductModel ReadProductModel()
    {
        Console.WriteLine("\n--- Enter Product Details ---");
        int id = ReadInt("Product ID (0 for new)");
        int productTitleId = ReadInt("Product Title ID");
        int manufacturerId = ReadInt("Manufacturer ID");
        decimal unitPrice = ReadDecimal("Unit Price");
        int availableQuantity = ReadInt("Available Quantity");

        return new ProductModel(id, productTitleId, manufacturerId, unitPrice, availableQuantity);
    }

    public static CustomerOrderModel ReadCustomerOrderModel()
    {
        Console.WriteLine("\n--- Enter Customer Order Details ---");
        int id = ReadInt("Order ID (0 for new)");
        DateTime operationTime = DateTime.Now;
        int customerId = ReadInt("Customer ID");
        int orderStateId = ReadInt("Order State ID");

        return new CustomerOrderModel(id, operationTime, customerId, orderStateId);
    }

    public static OrderDetailModel ReadOrderDetailModel()
    {
        Console.WriteLine("\n--- Enter Order Detail Details ---");
        int id = ReadInt("Order Detail ID (0 for new)");
        int orderId = ReadInt("Order ID");
        int productId = ReadInt("Product ID");
        int quantity = ReadInt("Quantity");
        decimal unitPrice = ReadDecimal("Unit Price");

        return new OrderDetailModel(id, orderId, productId, quantity, unitPrice);
    }
}