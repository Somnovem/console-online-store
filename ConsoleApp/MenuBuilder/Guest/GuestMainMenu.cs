namespace ConsoleApp.MenuBuilder.Guest;

using ConsoleApp.Controllers;
using System;

public class GuestMainMenu : AbstractMenuCreator
{
    private readonly ProductController productController;
    private readonly UserController userController;

    public GuestMainMenu(
        ProductController productController,
        UserController userController)
    {
        this.productController = productController ?? throw new ArgumentNullException(nameof(productController));
        this.userController = userController ?? throw new ArgumentNullException(nameof(userController));
    }

    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems()
    {
        (ConsoleKey id, string caption, Action action)[] array =
        [

            (ConsoleKey.D1, "Login", () => { }),
            (ConsoleKey.D2, "Show product list", this.productController.ShowAllProducts),
            (ConsoleKey.D3, "Register", this.userController.AddUser),
        ];
        return array;
    }
}