namespace ConsoleApp.MenuBuilder.Admin;

using ConsoleApp.Controllers;
using System;

public class AdminMainMenu : AbstractMenuCreator
{
    private readonly UserController userController;
    private readonly ShopController shopController;
    private readonly ProductController productController;
    private readonly DatabaseController databaseController;

    public AdminMainMenu(
        UserController userController,
        ShopController shopController,
        ProductController productController,
        DatabaseController databaseController)
    {
        this.userController = userController ?? throw new ArgumentNullException(nameof(userController));
        this.shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
        this.productController = productController ?? throw new ArgumentNullException(nameof(productController));
        this.databaseController = databaseController ?? throw new ArgumentNullException(nameof(databaseController));
    }

    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
            {
                (ConsoleKey.D1, "Logout", () => { }),
                (ConsoleKey.D2, "Show product list", this.productController.ShowAllProducts),
                (ConsoleKey.D3, "Add product", this.productController.AddProduct),
                (ConsoleKey.D4, "Show order list", this.shopController.ShowAllOrders),
                (ConsoleKey.D5, "Cancel order", this.shopController.DeleteOrder),
                (ConsoleKey.D6, "Change order status", this.shopController.ProcessOrder),
                (ConsoleKey.D7, "User roles", this.userController.ShowAllUserRoles),
                (ConsoleKey.D8, "Order states", this.shopController.ShowAllOrderStates),
                (ConsoleKey.D9, "Drop database", this.databaseController.DropDatabase),
            };
        return array;
    }
}
