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
    /// Service class for managing product titles.
    /// Implements CRUD operations via <see cref="ICrud"/>.
    /// </summary>
    public class ProductTitleService : ICrud
    {
        private readonly IProductTitleRepository productTitleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTitleService"/> class.
        /// </summary>
        /// <param name="productTitleRepository">Repository for accessing product titles.</param>
        /// <exception cref="ArgumentNullException">Thrown if repository is null.</exception>
        public ProductTitleService(IProductTitleRepository productTitleRepository)
        {
            this.productTitleRepository = productTitleRepository ?? throw new ArgumentNullException(nameof(productTitleRepository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not ProductTitleModel productTitleModel)
            {
                throw new ArgumentException("Model is not a ProductTitleModel.", nameof(model));
            }

            var entity = MapToEntity(productTitleModel) ?? throw new InvalidOperationException("Failed to map model to entity.");
            this.productTitleRepository.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.productTitleRepository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.productTitleRepository
                .GetAll()
                .Select(MapToModel)
                .Where(m => m != null)
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.productTitleRepository.GetById(id)
                ?? throw new InvalidOperationException($"ProductTitle with ID {id} not found.");

            return MapToModel(entity) !;
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not ProductTitleModel productTitleModel)
            {
                throw new ArgumentException("Model is not a ProductTitleModel.", nameof(model));
            }

            var entity = MapToEntity(productTitleModel) ?? throw new InvalidOperationException("Failed to map model to entity.");
            this.productTitleRepository.Update(entity);
        }

        /// <summary>
        /// Maps a <see cref="ProductTitle"/> entity to a <see cref="ProductTitleModel"/>.
        /// </summary>
        private static ProductTitleModel? MapToModel(ProductTitle? entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ProductTitleModel(entity.Id, entity.Title, entity.CategoryId);
        }

        /// <summary>
        /// Maps a <see cref="ProductTitleModel"/> to a <see cref="ProductTitle"/> entity.
        /// </summary>
        private static ProductTitle? MapToEntity(ProductTitleModel? model)
        {
            if (model == null)
            {
                return null;
            }

            return new ProductTitle(model.Id, model.Title, model.CategoryId);
        }
    }
}
