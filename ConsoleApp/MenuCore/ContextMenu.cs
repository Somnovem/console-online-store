using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Controllers;
using ConsoleApp.Handlers.ContextMenuHandlers;
using ConsoleApp1;
using StoreBLL.Interfaces;
using StoreBLL.Models;

namespace ConsoleApp.MenuCore;

public class ContextMenu : Menu
{
    private readonly Func<IEnumerable<AbstractModel>> getAll;

    public ContextMenu(ContextMenuHandler controller, Func<IEnumerable<AbstractModel>> getAll)
        : base(controller?.GenerateMenuItems() !)
    {
        private readonly Func<IEnumerable<AbstractModel>> getAll;

        public ContextMenu(AdminContextMenuHandler controller, Func<IEnumerable<AbstractModel>> getAll)
            : base(controller?.GenerateMenuItems() !)
        {
            ArgumentNullException.ThrowIfNull(controller);
            this.getAll = getAll;
        }

    public override void Run()
    {
        ConsoleKey resKey;
        bool updateItems = true;
        do
        {
            if (updateItems)
            {
                Console.WriteLine("======= Current DataSet ==========");
                foreach (var record in this.getAll())
                {
                    Console.WriteLine(record);
                }

                Console.WriteLine("===================================");
            }

            resKey = this.RunOnce(ref updateItems);
        }
        while (resKey != ConsoleKey.Q);
    }
}