namespace StoreDAL.Data.InitDataFactory;
using System;
using StoreDAL.Entities;

public class TestDataFactory : IDataFactory
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
        return Array.Empty<CustomerOrder>();
    }

    public Manufacturer[] GetManufacturerData()
    {
        return Array.Empty<Manufacturer>();
    }

    public OrderDetail[] GetOrderDetailData()
    {
        return Array.Empty<OrderDetail>();
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
        return Array.Empty<Product>();
    }

    public ProductTitle[] GetProductTitleData()
    {
        return Array.Empty<ProductTitle>();
    }

    public User[] GetUserData()
    {
        return Array.Empty<User>();
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
