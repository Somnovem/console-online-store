namespace ConsoleApp.Handlers.ContextMenuHandlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreBLL.Interfaces;
using StoreBLL.Models;

public abstract class ContextMenuHandler
{
    protected readonly ICrud Service;
    protected readonly Func<AbstractModel> ReadModel;

    protected ContextMenuHandler(ICrud service, Func<AbstractModel> readModel)
    {
        this.Service = service;
        this.ReadModel = readModel;
    }

    public void GetItemDetails()
    {
        Console.WriteLine("Input record ID for more details");
        int id = int.Parse(Console.ReadLine() !, CultureInfo.InvariantCulture);
        Console.WriteLine(this.Service.GetById(id));
    }

    public abstract (ConsoleKey id, string caption, Action action)[] GenerateMenuItems();
}
