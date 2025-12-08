using ConsoleApp.Helpers;
using StoreBLL.Interfaces;
using StoreBLL.Models;

namespace ConsoleApp.MenuBuilder
{
    public abstract class ContextMenuHandler<T>
        where T : class, ICrud
    {
        protected readonly T service;
        protected readonly Func<AbstractModel> readModel;

        protected ContextMenuHandler(T service, Func<AbstractModel> readModel)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.readModel = readModel ?? throw new ArgumentNullException(nameof(readModel));
        }

        public abstract (ConsoleKey id, string caption, Action action)[] GenerateMenuItems();

        protected void GetItemDetails()
        {
            var id = InputHelper.ReadInt("Enter ID to view details");
            var item = this.service.GetById(id);

            if (item != null)
            {
                Console.WriteLine($"\n--- Details for ID: {id} ---");
                Console.WriteLine(item.ToString());
            }
            else
            {
                Console.WriteLine($"\nNo item found with ID: {id}");
            }

            InputHelper.PressAnyKeyToContinue();
        }
    }
}