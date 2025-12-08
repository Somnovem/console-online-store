namespace ConsoleApp.MenuBuilder;

public interface IMenuCreator
{
    (ConsoleKey id, string caption, Action action)[] GetMenuItems();
}
