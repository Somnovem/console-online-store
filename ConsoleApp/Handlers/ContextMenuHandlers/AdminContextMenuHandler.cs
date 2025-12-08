using ConsoleApp.Helpers;
using StoreBLL.Models;
using StoreBLL.Services;

namespace ConsoleApp.MenuBuilder
{
    public class AdminContextMenuHandler : ContextMenuHandler<ProductService>
    {
        public AdminContextMenuHandler(ProductService service, Func<AbstractModel> readModel)
            : base(service, readModel)
        {
        }

        public void AddItem()
        {
            Console.WriteLine("\n--- Add New Item ---");
            try
            {
                var item = this.readModel();
                this.service.Add(item);
                Console.WriteLine("Item added successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error adding item: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error adding item: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error adding item: {ex.Message}");
                throw;
            }
            finally
            {
                InputHelper.PressAnyKeyToContinue();
            }
        }

        public void RemoveItem()
        {
            Console.WriteLine("\n--- Remove Item ---");
            try
            {
                int id = InputHelper.ReadInt("record ID that will be removed");
                this.service.Delete(id);
                Console.WriteLine($"Item with ID {id} removed successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error removing item: {ex.Message}");
                throw;
            }
            finally
            {
                InputHelper.PressAnyKeyToContinue();
            }
        }

        public void EditItem()
        {
            Console.WriteLine("\n--- Edit Item ---");
            try
            {
                int id = InputHelper.ReadInt("Item ID to update");
                var existingItem = this.service.GetById(id);

                if (existingItem == null)
                {
                    Console.WriteLine($"Item with ID {id} not found.");
                    return;
                }

                Console.WriteLine($"\nEditing Item (ID: {existingItem.Id}, Current: {existingItem.ToString()})");

                var updatedItem = this.readModel();
                updatedItem.Id = id;

                this.service.Update(updatedItem);
                Console.WriteLine($"Item with ID {id} updated successfully!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error updating item: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error updating item: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error updating item: {ex.Message}");
                throw;
            }
            finally
            {
                InputHelper.PressAnyKeyToContinue();
            }
        }

        public override (ConsoleKey id, string caption, Action action)[] GenerateMenuItems()
        {
            return new (ConsoleKey, string, Action)[]
            {
                (ConsoleKey.A, "Add Item", this.AddItem),
                (ConsoleKey.R, "Remove Item", this.RemoveItem),
                (ConsoleKey.E, "Edit Item", this.EditItem),
                (ConsoleKey.V, "View Details", this.GetItemDetails),
            };
        }
    }
}
