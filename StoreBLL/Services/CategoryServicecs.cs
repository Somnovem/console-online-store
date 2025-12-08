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
    /// Provides CRUD operations for managing product categories.
    /// </summary>
    public class CategoryService : ICrud
    {
        private readonly ICategoryRepository categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        /// <param name="categoryRepository">The repository used for category data access.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="categoryRepository"/> is null.</exception>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="model">The category model to add.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is not a <see cref="CategoryModel"/>.</exception>
        public void Add(AbstractModel model)
        {
            if (model is not CategoryModel categoryModel)
            {
                throw new ArgumentException("Model is not a CategoryModel.", nameof(model));
            }

            var categoryEntity = MapToEntity(new CategoryModel(0, categoryModel.CategoryName));
            if (categoryEntity != null)
            {
                this.categoryRepository.Add(categoryEntity);
            }
        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="modelId">The ID of the category to delete.</param>
        public void Delete(int modelId)
        {
            this.categoryRepository.DeleteById(modelId);
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A collection of <see cref="CategoryModel"/> instances.</returns>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.categoryRepository.GetAll()
                .Select(MapToModel)
                .Where(model => model != null)
                .ToList()!;
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The corresponding <see cref="CategoryModel"/>, or null if not found.</returns>
        public AbstractModel? GetById(int id)
        {
            var categoryEntity = this.categoryRepository.GetById(id);
            return MapToModel(categoryEntity);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="model">The category model containing updated data.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="model"/> is not a <see cref="CategoryModel"/>.</exception>
        public void Update(AbstractModel model)
        {
            if (model is not CategoryModel categoryModel)
            {
                throw new ArgumentException("Model is not a CategoryModel.", nameof(model));
            }

            var categoryEntity = MapToEntity(categoryModel);
            if (categoryEntity != null)
            {
                this.categoryRepository.Update(categoryEntity);
            }
        }

        /// <summary>
        /// Maps a <see cref="Category"/> entity to a <see cref="CategoryModel"/>.
        /// </summary>
        /// <param name="entity">The category entity.</param>
        /// <returns>The mapped <see cref="CategoryModel"/>, or null if <paramref name="entity"/> is null.</returns>
        private static CategoryModel? MapToModel(Category entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CategoryModel(entity.Id, entity.CategoryName);
        }

        /// <summary>
        /// Maps a <see cref="CategoryModel"/> to a <see cref="Category"/> entity.
        /// </summary>
        /// <param name="model">The category model.</param>
        /// <returns>The mapped <see cref="Category"/> entity, or null if <paramref name="model"/> is null.</returns>
        private static Category? MapToEntity(CategoryModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Category(model.Id, model.CategoryName);
        }
    }
}
