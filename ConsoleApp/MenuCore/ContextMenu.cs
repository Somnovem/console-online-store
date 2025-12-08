using StoreBLL.Models;

namespace ConsoleApp.MenuCore;

public class ContextMenu : Menu
{
    private readonly Func<IEnumerable<AbstractModel>> getAll;

    public ContextMenu(Func<(ConsoleKey id, string caption, Action action)[]> generateMenuItems, Func<IEnumerable<AbstractModel>> getAll)
        : base(generateMenuItems())
    {
        this.getAll = getAll ?? throw new ArgumentNullException(nameof(getAll));
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

                var dataSet = this.getAll();
                if (dataSet != null && dataSet.Any())
                {
                    foreach (var record in dataSet)
                    {
                        Console.WriteLine(record);
                    }
                }
                else
                {
                    Console.WriteLine("No data available to display.");
                }

                Console.WriteLine("==================================");
                Console.WriteLine();
            }

            resKey = this.RunOnce(ref updateItems);
        }
        while (resKey != ConsoleKey.Escape);
    }
}
