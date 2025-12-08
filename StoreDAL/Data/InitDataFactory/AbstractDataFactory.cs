namespace StoreDAL.Data.InitDataFactory;

using StoreDAL.Entities;
using StoreDAL.Data;

public interface IDataFactory
{
    Category[] GetCategoryData();

    CustomerOrder[] GetCustomerOrderData();

    Manufacturer[] GetManufacturerData();

    OrderDetail[] GetOrderDetailData();

    OrderState[] GetOrderStateData();

    Product[] GetProductData();

    ProductTitle[] GetProductTitleData();

    User[] GetUserData();

    UserRole[] GetUserRoleData();
}
