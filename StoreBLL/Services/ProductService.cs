namespace StoreBLL.Services;

using StoreDAL.Entities;
using StoreDAL.Repository;
using System.Collections.Generic;
using Interfaces;
using Models;
using StoreDAL.Data;
using StoreDAL.Interfaces;

/// <summary>
/// Service for managing products, including CRUD operations.
/// </summary>
public class ProductService : ICrud
{
    private readonly IProductRepository repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class with the given database context.
    /// </summary>
    /// <param name="context">The database context used to access product data.</param>
    public ProductService(StoreDbContext context)
    {
        this.repository = new ProductRepository(context);
    }

    /// <summary>
    /// Adds a new product to the repository.
    /// </summary>
    /// <param name="model">The <see cref="ProductModel"/> containing product information.</param>
    public void Add(AbstractModel model)
    {
        var productModel = (ProductModel)model;
        this.repository.Add(new Product(
            productModel.Id,
            productModel.TitleId,
            productModel.ManufacturerId,
            productModel.Description,
            productModel.UnitPrice));
    }

    /// <summary>
    /// Deletes a product from the repository by ID.
    /// </summary>
    /// <param name="modelId">The ID of the product to delete.</param>
    public void Delete(int modelId)
    {
        var entity = this.repository.GetById(modelId);
        this.repository.Delete(entity);
    }

    /// <summary>
    /// Retrieves all products from the repository.
    /// </summary>
    /// <returns>A collection of <see cref="ProductModel"/> instances.</returns>
    public IEnumerable<AbstractModel> GetAll()
    {
        return this.repository.GetAll().Select(p => new ProductModel(
            p.Id,
            p.TitleId,
            p.ManufacturerId,
            p.Description,
            p.UnitPrice));
    }

    /// <summary>
    /// Retrieves a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The <see cref="ProductModel"/> corresponding to the ID.</returns>
    public AbstractModel GetById(int id)
    {
        var p = this.repository.GetById(id);
        return new DetailedProductModel(
            p.Id,
            new ProductTitleModel(p.Title.Id, p.Title.Title),
            new ManufacturerModel(p.Manufacturer.Id, p.Manufacturer.Name),
            p.Description,
            p.UnitPrice);
    }

    /// <summary>
    /// Updates a product's information in the repository.
    /// </summary>
    /// <param name="model">The <see cref="ProductModel"/> containing updated product information.</param>
    public void Update(AbstractModel model)
    {
        ProductModel productModel = (ProductModel)model;

        this.repository.Update(new Product(
            productModel.Id,
            productModel.TitleId,
            productModel.ManufacturerId,
            productModel.Description,
            productModel.UnitPrice));
    }
}
