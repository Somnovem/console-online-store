namespace StoreDAL.Data.InitDataFactory;
using System;
using StoreDAL.Entities;

public class ReleaseDataFactory : IDataFactory
{
    public Category[] GetCategoryData()
    {
        return Array.Empty<Category>();
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
            new OrderState(2, "Canceled by user"),
            new OrderState(3, "Canceled by administrator"),
            new OrderState(4, "Confirmed"),
            new OrderState(5, "Moved to delivery company"),
            new OrderState(6, "In delivery"),
            new OrderState(7, "Delivered to client"),
            new OrderState(8, "Delivery aproved by client"),
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
