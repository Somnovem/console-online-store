namespace StoreBLL.Interfaces;

using System;

/// <summary>
/// Represents a factory for building collections of console menu items.
/// </summary>
public interface IMenuCreator
{
    /// <summary>
    /// Produces the set of menu items to display.
    /// </summary>
    /// <returns>
    /// An array of tuples, each describing a single menu item.
    /// Each tuple includes:
    /// <list type="bullet">
    /// <item><description><c>id</c>: The <see cref="ConsoleKey"/> associated with the menu option.</description></item>
    /// <item><description><c>caption</c>: The label shown to the user.</description></item>
    /// <item><description><c>action</c>: The operation executed when the item is chosen.</description></item>
    /// </list>
    /// </returns>
    (ConsoleKey id, string caption, Action action)[] GetMenuItems();
}
