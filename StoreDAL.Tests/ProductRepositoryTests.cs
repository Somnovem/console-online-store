using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using StoreDAL.Repository;
using Xunit;

namespace StoreDAL.Tests;

public class ProductRepositoryTests : IDisposable
{
    private readonly StoreDbContext context;
    private readonly IProductRepository repository;

    public ProductRepositoryTests()
    {
        var factory = new StoreDbFactory(new TestDataFactory());
        context = factory.CreateContext();
        this.repository = new ProductRepository(context);
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldCreateInstance()
    {
        // Arrange & Act
        var repo = new ProductRepository(context);

        // Assert
        Assert.NotNull(repo);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ProductRepository(null));
    }

    [Fact]
    public void Add_WithValidProduct_ShouldAddProductToDatabase()
    {
        // Arrange
        var newProduct = new Product(100, 1, 1, "Test product description", 99.99m);
        var initialCount = context.Products.Count();

        // Act
        repository.Add(newProduct);

        // Assert
        var actualCount = context.Products.Count();
        Assert.Equal(initialCount + 1, actualCount);

        var addedProduct = context.Products.Find(100);
        Assert.NotNull(addedProduct);
        Assert.Equal("Test product description", addedProduct.Description);
        Assert.Equal(99.99m, addedProduct.UnitPrice);
    }

    [Fact]
    public void Add_WithNullProduct_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Add(null));
    }

    [Fact]
    public void GetAll_ShouldReturnAllProducts()
    {
        // Act
        var products = repository.GetAll();

        // Assert
        Assert.NotNull(products);
        Assert.Equal(29, products.Count()); // Based on test data factory
    }

    [Fact]
    public void GetAll_WithPagination_ShouldReturnCorrectPage()
    {
        // Arrange
        int pageNumber = 2;
        int rowCount = 5;

        // Act
        var products = repository.GetAll(pageNumber, rowCount);

        // Assert
        Assert.NotNull(products);
        var enumerable = products as Product[] ?? products.ToArray();
        Assert.Equal(5, enumerable.Count());
            
        // Should skip first 5 products and take next 5
        var productList = enumerable.ToList();
        Assert.Equal(6, productList[0].Id); // 6th product should be first in page 2
    }

    [Fact]
    public void GetAll_WithInvalidPageNumber_ShouldDefaultToPage1()
    {
        // Arrange
        int invalidPageNumber = 0;
        int rowCount = 3;

        // Act
        var products = repository.GetAll(invalidPageNumber, rowCount);

        // Assert
        Assert.NotNull(products);
        var enumerable = products as Product[] ?? products.ToArray();
        Assert.Equal(3, enumerable.Count());
            
        var productList = enumerable.ToList();
        Assert.Equal(1, productList[0].Id); // Should start from first product
    }

    [Fact]
    public void GetAll_WithInvalidRowCount_ShouldDefaultTo10()
    {
        // Arrange
        int pageNumber = 1;
        int invalidRowCount = 0;

        // Act
        var products = repository.GetAll(pageNumber, invalidRowCount);

        // Assert
        Assert.NotNull(products);
        Assert.Equal(10, products.Count()); // Should default to 10 rows
    }

    [Fact]
    public void GetById_WithValidId_ShouldReturnProductWithRelatedData()
    {
        // Arrange
        int productId = 1; // Apple from test data

        // Act
        var product = repository.GetById(productId);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
        Assert.NotNull(product.Title);
        Assert.NotNull(product.Manufacturer);
        Assert.Equal("Apple", product.Title.Title);
        Assert.Equal("Fresh Farms", product.Manufacturer.Name);
    }

    [Fact]
    public void GetById_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        int invalidId = 999;

        // Act
        var product = repository.GetById(invalidId);

        // Assert
        Assert.Null(product);
    }

    [Fact]
    public void Update_WithValidProduct_ShouldUpdateProductInDatabase()
    {
        // Arrange
        var existingProduct = context.Products.Find(1);
        Assert.NotNull(existingProduct);
            
        var updatedProduct = new Product(1, existingProduct.TitleId, existingProduct.ManufacturerId, "Updated description", 5.99m);

        // Act
        repository.Update(updatedProduct);

        // Assert
        var retrievedProduct = context.Products.Find(1);
        Assert.NotNull(retrievedProduct);
        Assert.Equal("Updated description", retrievedProduct.Description);
        Assert.Equal(5.99m, retrievedProduct.UnitPrice);
    }

    [Fact]
    public void Update_WithNullProduct_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Update(null));
    }

    [Fact]
    public void Update_WithNonExistentProduct_ShouldNotThrow()
    {
        // Arrange
        var nonExistentProduct = new Product(999, 1, 1, "Non-existent product", 10.00m);

        // Act & Assert
        // Should not throw exception, just do nothing
        repository.Update(nonExistentProduct);
            
        // Verify it wasn't added
        var product = context.Products.Find(999);
        Assert.Null(product);
    }

    [Fact]
    public void Delete_WithValidProduct_ShouldRemoveProductFromDatabase()
    {
        // Arrange
        var productToDelete = context.Products.Find(29); // Beer from test data
        Assert.NotNull(productToDelete);
        var initialCount = context.Products.Count();

        // Act
        repository.Delete(productToDelete);

        // Assert
        var actualCount = context.Products.Count();
        Assert.Equal(initialCount - 1, actualCount);
            
        var deletedProduct = context.Products.Find(29);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public void Delete_WithNullProduct_ShouldThrowArgumentNullException()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
    }

    [Fact]
    public void DeleteById_WithValidId_ShouldRemoveProductFromDatabase()
    {
        // Arrange
        int productIdToDelete = 28; // Red Wine from test data
        var initialCount = context.Products.Count();

        // Act
        repository.DeleteById(productIdToDelete);

        // Assert
        var actualCount = context.Products.Count();
        Assert.Equal(initialCount - 1, actualCount);
            
        var deletedProduct = context.Products.Find(productIdToDelete);
        Assert.Null(deletedProduct);
    }

    [Fact]
    public void DeleteById_WithInvalidId_ShouldNotThrowAndNotChangeCount()
    {
        // Arrange
        int invalidId = 999;
        var initialCount = context.Products.Count();

        // Act
        repository.DeleteById(invalidId);

        // Assert
        var actualCount = context.Products.Count();
        Assert.Equal(initialCount, actualCount);
    }

    [Fact]
    public void GetAll_ShouldReturnProductsOrderedById()
    {
        // Act
        var products = repository.GetAll(1, 5);

        // Assert
        var productList = products.ToList();
        Assert.True(productList[0].Id < productList[1].Id);
        Assert.True(productList[1].Id < productList[2].Id);
        Assert.True(productList[2].Id < productList[3].Id);
        Assert.True(productList[3].Id < productList[4].Id);
    }

    [Fact]
    public void GetById_ShouldIncludeManufacturerAndTitleData()
    {
        // Arrange
        int productId = 19; // iPhone from test data

        // Act
        var product = repository.GetById(productId);

        // Assert
        Assert.NotNull(product);
        Assert.NotNull(product.Manufacturer);
        Assert.NotNull(product.Title);
        Assert.Equal("Apple Inc.", product.Manufacturer.Name);
        Assert.Equal("iPhone", product.Title.Title);
        Assert.Equal(8, product.Title.CategoryId); // smartphones category
    }

    [Theory]
    [InlineData(1, 5, 5)] // Page 1, 5 items
    [InlineData(2, 10, 10)] // Page 2, 10 items
    [InlineData(3, 15, 0)] // Page 3, 15 items (only 14 remaining)
    [InlineData(4, 10, 0)] // Page 4, 10 items (no items left)
    public void GetAll_WithDifferentPaginationParameters_ShouldReturnCorrectCount(int pageNumber, int rowCount, int expectedCount)
    {
        // Act
        var products = repository.GetAll(pageNumber, rowCount);

        // Assert
        Assert.Equal(expectedCount, products.Count());
    }

    public void Dispose()
    {
        context.Dispose();
    }
}