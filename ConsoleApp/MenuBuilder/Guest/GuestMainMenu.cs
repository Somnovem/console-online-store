using ConsoleApp.Controllers;
using StoreDAL.Data;

namespace ConsoleApp.MenuBuilder.Guest;

public class GuestMainMenu : AbstractMenuCreator
{
    public override (ConsoleKey id, string caption, Action action)[] GetMenuItems(StoreDbContext context)
    {
        (ConsoleKey id, string caption, Action action)[] array =
        [
            (ConsoleKey.F1, "Login", UserMenuController.Login),
            (ConsoleKey.F2, "Show product list", ProductController.ShowAllProducts),
            (ConsoleKey.F3, "Register", UserMenuController.Register)
        ];
        return array;
    }
}