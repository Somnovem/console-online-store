namespace StoreBLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StoreBLL.Interfaces;
    using StoreBLL.Models;
    using StoreDAL.Entities;
    using StoreDAL.Interfaces;

    /// <summary>
    /// Provides CRUD operations for managing products.
    /// Implements <see cref="ICrud"/> interface.
    /// </summary>
    public class ProductService : ICrud
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The repository for accessing product data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="productRepository"/> is null.</exception>
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not ProductModel productModel)
            {
                throw new ArgumentException("Model is not a ProductModel.", nameof(model));
            }

            var entity = MapToEntity(productModel) ?? throw new InvalidOperationException("Mapping to entity failed.");
            this.productRepository.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.productRepository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.productRepository
                .GetAll()
                .Select(MapToModel)
                .Where(m => m != null)
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.productRepository.GetById(id)
                ?? throw new InvalidOperationException($"Product with ID {id} not found.");

            return MapToModel(entity) !;
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not ProductModel productModel)
            {
                throw new ArgumentException("Model is not a ProductModel.", nameof(model));
            }

            var entity = MapToEntity(productModel) ?? throw new InvalidOperationException("Mapping to entity failed.");
            this.productRepository.Update(entity);
        }

        /// <summary>
        /// Maps a <see cref="Product"/> entity to a <see cref="ProductModel"/>.
        /// </summary>
        private static ProductModel? MapToModel(Product? entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ProductModel(
                entity.Id,
                entity.ProductTitleId,
                entity.ManufacturerId,
                entity.UnitPrice,
                entity.AvailableQuantity);
        }

        /// <summary>
        /// Maps a <see cref="ProductModel"/> to a <see cref="Product"/> entity.
        /// </summary>
        private static Product? MapToEntity(ProductModel? model)
        {
            if (model == null)
            {
                return null;
            }

            return new Product(
                model.Id,
                model.ProductTitleId,
                model.ManufacturerId,
                string.Empty, // Assuming the Product entity requires a Title string which is set elsewhere
                model.UnitPrice,
                model.AvailableQuantity);
        }
    }
}
