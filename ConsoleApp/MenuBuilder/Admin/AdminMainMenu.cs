namespace ConsoleApp.MenuBuilder.Admin;

using ConsoleApp.Controllers;
using System;

public class AdminMainMenu : AbstractMenuCreator
{
    private readonly UserController userController;
    private readonly ShopController shopController;
    private readonly ProductController productController;

    public AdminMainMenu(
        UserController userController,
        ShopController shopController,
        ProductController productController)
    {
        this.userController = userController ?? throw new ArgumentNullException(nameof(userController));
        this.shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
        this.productController = productController ?? throw new ArgumentNullException(nameof(productController));
    }

    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.F1, "Logout", () => { }),
                (ConsoleKey.F2, "Show product list", this.productController.ShowAllProducts),
                (ConsoleKey.F3, "Add product", this.productController.AddProduct),
                (ConsoleKey.F4, "Show order list", this.shopController.ShowAllOrders),
                (ConsoleKey.F5, "Cancel order", this.shopController.DeleteOrder),
                (ConsoleKey.F6, "Change order status", this.shopController.ProcessOrder),
                (ConsoleKey.F7, "User roles", this.userController.ShowAllUserRoles),
                (ConsoleKey.F8, "Order states", this.shopController.ShowAllOrderStates),
            };
        return array;
    }
}
