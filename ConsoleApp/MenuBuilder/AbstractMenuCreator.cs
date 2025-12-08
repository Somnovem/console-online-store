using ConsoleApp.MenuCore;

namespace ConsoleApp.MenuBuilder;

public abstract class AbstractMenuCreator
{
    public abstract (ConsoleKey id, string caption, Action action)[] GetMenuItems();

    public Menu CreateMenu()
    {
        return new Menu(this.GetMenuItems());
    }
}
