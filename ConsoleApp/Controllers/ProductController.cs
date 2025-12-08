namespace ConsoleApp.Controllers;

using System;
using StoreBLL.Services;
using ConsoleApp.Helpers;
using System.Linq;

public class ProductController
{
    private readonly ProductService productService;
    private readonly CategoryService categoryService;
    private readonly ManufacturerService manufacturerService;
    private readonly ProductTitleService productTitleService;

    public ProductController(
        ProductService productService,
        CategoryService categoryService,
        ManufacturerService manufacturerService,
        ProductTitleService productTitleService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this.manufacturerService = manufacturerService ?? throw new ArgumentNullException(nameof(manufacturerService));
        this.productTitleService = productTitleService ?? throw new ArgumentNullException(nameof(productTitleService));
    }

    public void AddProduct()
    {
        Console.WriteLine("\n--- Add New Product ---");
        try
        {
            var product = InputHelper.ReadProductModel();
            this.productService.Add(product);
            Console.WriteLine("Product added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateProduct()
    {
        Console.WriteLine("\n--- Update Product ---");
        try
        {
            int id = InputHelper.ReadInt("Product ID to update");
            var existingProduct = this.productService.GetById(id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Product with ID {id} not found.");
                return;
            }

            Console.WriteLine("Enter new product details (leave blank to keep current value):");
            var updatedProduct = InputHelper.ReadProductModel();
            updatedProduct.Id = id;

            this.productService.Update(updatedProduct);
            Console.WriteLine($"Product with ID {id} updated succesfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteProduct()
    {
        Console.WriteLine("\n--- Delete Product ---");
        try
        {
            int id = InputHelper.ReadInt("Product ID to delete");
            this.productService.Delete(id);
            Console.WriteLine($"Product with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowProduct()
    {
        Console.WriteLine("\n--- Show Product Details ---");
        try
        {
            int id = InputHelper.ReadInt("Product ID to view");
            var product = this.productService.GetById(id);
            if (product != null)
            {
                Console.WriteLine(product.ToString());
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing product: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllProducts()
    {
        Console.WriteLine("\n--- All Products ---");
        try
        {
            var products = this.productService.GetAll().ToList();
            if (products.Count != 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine(product.ToString());
                }
            }
            else
            {
                Console.WriteLine("No products found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all products: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void AddCategory()
    {
        Console.WriteLine("\n--- Add New Category ---");
        try
        {
            var category = InputHelper.ReadCategoryModel();
            this.categoryService.Add(category);
            Console.WriteLine("Category added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding category: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateCategory()
    {
        Console.WriteLine("\n--- Update Category ---");
        try
        {
            int id = InputHelper.ReadInt("Category ID to update");
            var existingCategory = this.categoryService.GetById(id);
            if (existingCategory == null)
            {
                Console.WriteLine($"Category with ID {id} not foun.");
                return;
            }

            var updatedCategory = InputHelper.ReadCategoryModel();
            updatedCategory.Id = id;
            this.categoryService.Update(updatedCategory);
            Console.WriteLine($"Category with ID {id} updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating category {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteCategory()
    {
        Console.WriteLine("\n--- Delete Category ---");
        try
        {
            int id = InputHelper.ReadInt("Category ID to delete");
            this.categoryService.Delete(id);
            Console.WriteLine($"Category with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting category: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllCategories()
    {
        Console.WriteLine("\n--- All Categories ---");
        try
        {
            var categories = this.categoryService.GetAll().ToList();
            if (categories.Count != 0)
            {
                foreach (var category in categories)
                {
                    Console.WriteLine(category.ToString());
                }
            }
            else
            {
                Console.WriteLine("No categories found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all categories: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void AddProductTitle()
    {
        Console.WriteLine("\n--- Add New Product Title ---");
        try
        {
            var productTitle = InputHelper.ReadProductTitleModel();
            this.productTitleService.Add(productTitle);
            Console.WriteLine("Product Title added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding product title: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateProductTitle()
    {
        Console.WriteLine("\n--- Update Product Title ---");
        try
        {
            int id = InputHelper.ReadInt("Product Title ID to update");
            var existingTitle = this.productTitleService.GetById(id);
            if (existingTitle == null)
            {
                Console.WriteLine($"Product Title with ID {id} not found.");
                return;
            }

            var updatedTitle = InputHelper.ReadProductTitleModel();
            updatedTitle.Id = id;
            this.productTitleService.Update(updatedTitle);
            Console.WriteLine($"Product Title with ID {id} updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating product title {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteProductTitle()
    {
        Console.WriteLine("\n--- Delete Product Title ---");
        try
        {
            int id = InputHelper.ReadInt("Product Title ID to delete");
            this.productTitleService.Delete(id);
            Console.WriteLine($"Product Title with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting product title: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllProductTitles()
    {
        Console.WriteLine("\n--- All Product Titles ---");
        try
        {
            var titles = this.productTitleService.GetAll().ToList();
            if (titles.Count != 0)
            {
                foreach (var title in titles)
                {
                    Console.WriteLine(title.ToString());
                }
            }
            else
            {
                Console.WriteLine("No product titles found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all product titles: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void AddManufacturer()
    {
        Console.WriteLine("\n--- Add New Manufacturer ---");
        try
        {
            var manufacturer = InputHelper.ReadManufacturerModel();
            this.manufacturerService.Add(manufacturer);
            Console.WriteLine("Manufacturer added succesfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding manufacturer: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateManufacturer()
    {
        Console.WriteLine("\n--- Update Manufacturer ---");
        try
        {
            int id = InputHelper.ReadInt("Manufacturer ID to update");
            var existingManufacturer = this.manufacturerService.GetById(id);
            if (existingManufacturer == null)
            {
                Console.WriteLine($"Manufacturer with ID {id} not found.");
                return;
            }

            var updatedManufacturer = InputHelper.ReadManufacturerModel();
            updatedManufacturer.Id = id;
            this.manufacturerService.Update(updatedManufacturer);
            Console.WriteLine($"Manufacturer with ID {id} updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating manufacturer: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteManufacturer()
    {
        Console.WriteLine("\n--- Delete Manufacturer ---");
        try
        {
            int id = InputHelper.ReadInt("Manufacturer ID to delete");
            this.manufacturerService.Delete(id);
            Console.WriteLine($"Manufacturer with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting manufacturer: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllManufacturers()
    {
        Console.WriteLine("\n--- All Manufacturers ---");
        try
        {
            var manufacturers = this.manufacturerService.GetAll().ToList();
            if (manufacturers.Count != 0)
            {
                foreach (var manufacturer in manufacturers)
                {
                    Console.WriteLine(manufacturer.ToString());
                }
            }
            else
            {
                Console.WriteLine("No manufacturers found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all manufacturers: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }
}
