using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.MenuBuilder
{
    public class GuestContextMenuHandler : ContextMenuHandler<ProductTitleService>
    {
        public GuestContextMenuHandler(
            ProductTitleService service,
            Func<AbstractModel> readModel)
            : base(service, readModel)
        {
        }

        public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
        {
            return new (ConsoleKey, string, Action)[]
            {
                (ConsoleKey.V, "View Details", this.GetItemDetails),
            };
        }
    }
}