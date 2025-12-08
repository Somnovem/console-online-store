using ConsoleApp.Controllers;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.MenuBuilder.Shop
{
    public class ShoppingContextMenuHandler : ContextMenuHandler<ProductService>
    {
        private readonly ShopController shopController;

        public ShoppingContextMenuHandler(
            ProductService service,
            ShopController shopController,
            Func<AbstractModel> readModel)
            : base(service, readModel)
        {
            this.shopController = shopController ?? throw new ArgumentNullException(nameof(shopController));
        }

        public void CreateOrder()
        {
            this.shopController.AddOrder();
            Console.WriteLine("Order creation process initiated.");
        }

        public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
        {
            return new (ConsoleKey, string, Action)[]
            {
                (ConsoleKey.V, "View Details", this.GetItemDetails),
                (ConsoleKey.A, "Add item to cart and create order", this.CreateOrder),
            };
        }
    }
}