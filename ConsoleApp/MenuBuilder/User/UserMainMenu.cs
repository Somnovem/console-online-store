namespace ConsoleApp.MenuBuilder.User;

using ConsoleApp.Controllers;
using System;
using System.Diagnostics;

public class UserMainMenu : AbstractMenuCreator
{
    private readonly ProductController productController;
    private readonly ShopController shopController;

    public UserMainMenu(
        ProductController productController,
        ShopController shopController)
    {
        this.productController = productController ?? throw new ArgumentNullException(nameof(productController));
        this.shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
    }

    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems()
    {
        Debug.WriteLine("UserMainMenu.GetMenuItems() is being called.");

        (ConsoleKey id, string caption, Action action)[] array =
        {
            (ConsoleKey.F1, "Logout", () => { }),
            (ConsoleKey.F2, "Show product list", this.productController.ShowAllProducts),
            (ConsoleKey.F3, "Show order list", this.shopController.ShowAllOrders),
            (ConsoleKey.F4, "Cancel order", this.shopController.DeleteOrder),
            (ConsoleKey.F5, "Confirm order delivery", () => { Console.WriteLine("Confirm order delivery - TODO"); }),
            (ConsoleKey.F6, "Add order feedback", () => { Console.WriteLine("Add order feedback - TODO"); }),
        };

        Debug.WriteLine($"Menu item count is: {array.Length}");

        return array;
    }
}
