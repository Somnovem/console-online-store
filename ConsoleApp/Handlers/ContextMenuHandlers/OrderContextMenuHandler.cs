using ConsoleApp.Controllers;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.MenuBuilder
{
    public class OrderContextMenuHandler : ContextMenuHandler<CustomerOrderService>
    {
        private readonly ShopController shopController;

        public OrderContextMenuHandler(
            CustomerOrderService service,
            ShopController shopController,
            Func<AbstractModel> readModel)
            : base(service, readModel)
        {
            this.shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
        }

        public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
        {
            return new (ConsoleKey, string, Action)[]
            {
                (ConsoleKey.V, "View Details", this.GetItemDetails),
                (ConsoleKey.C, "Change order status", this.shopController.ProcessOrder),
                (ConsoleKey.R, "Remove Order", this.shopController.DeleteOrder),
            };
        }
    }
}