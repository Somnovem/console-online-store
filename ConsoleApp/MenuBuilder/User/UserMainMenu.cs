namespace ConsoleApp.MenuBuilder.User;

using ConsoleApp.Controllers;
using ConsoleApp.Helpers;
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
            (ConsoleKey.D1, "Logout", () => { }),
            (ConsoleKey.D2, "Show product list", this.productController.ShowAllProducts),
            (ConsoleKey.D3, "Show order list", this.shopController.ShowAllOrders),
            (ConsoleKey.D4, "Cancel order", this.shopController.DeleteOrder),
            (ConsoleKey.D5, "Confirm order delivery", () =>
            {
                Console.WriteLine("\nConfirm order delivery - TODO");
                InputHelper.PressAnyKeyToContinue();
            }),
            (ConsoleKey.D6, "Add order feedback", () =>
            {
                Console.WriteLine("\nAdd order feedback - TODO");
                InputHelper.PressAnyKeyToContinue();
            }),
        };

        Debug.WriteLine($"Menu item count is: {array.Length}");

        return array;
    }
}
