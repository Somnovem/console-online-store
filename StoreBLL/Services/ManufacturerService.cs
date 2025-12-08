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
    /// Provides CRUD operations for managing manufacturers.
    /// Implements <see cref="ICrud"/> interface.
    /// </summary>
    public class ManufacturerService : ICrud
    {
        private readonly IManufacturerRepository manufacturerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerService"/> class.
        /// </summary>
        /// <param name="manufacturerRepository">Repository used for accessing manufacturer data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="manufacturerRepository"/> is null.</exception>
        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            this.manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not ManufacturerModel manufacturerModel)
            {
                throw new ArgumentException("Model is not a ManufacturerModel.", nameof(model));
            }

            var manufacturerEntity = MapToEntity(new ManufacturerModel(0, manufacturerModel.ManufacturerName));
            if (manufacturerEntity != null)
            {
                this.manufacturerRepository.Add(manufacturerEntity);
            }
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.manufacturerRepository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.manufacturerRepository.GetAll()
                .Select(MapToModel)
                .Where(model => model != null) !
                .ToList() !;
        }

        /// <inheritdoc/>
        public AbstractModel? GetById(int id)
        {
            var entity = this.manufacturerRepository.GetById(id);
            return MapToModel(entity);
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not ManufacturerModel manufacturerModel)
            {
                throw new ArgumentException("Model is not a ManufacturerModel.", nameof(model));
            }

            var manufacturerEntity = MapToEntity(manufacturerModel);
            if (manufacturerEntity != null)
            {
                this.manufacturerRepository.Update(manufacturerEntity);
            }
        }

        /// <summary>
        /// Maps a <see cref="Manufacturer"/> entity to a <see cref="ManufacturerModel"/>.
        /// </summary>
        /// <param name="entity">The manufacturer entity to map.</param>
        /// <returns>The corresponding <see cref="ManufacturerModel"/>, or null if <paramref name="entity"/> is null.</returns>
        private static ManufacturerModel? MapToModel(Manufacturer? entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ManufacturerModel(entity.Id, entity.ManufacturerName);
        }

        /// <summary>
        /// Maps a <see cref="ManufacturerModel"/> to a <see cref="Manufacturer"/> entity.
        /// </summary>
        /// <param name="model">The manufacturer model to map.</param>
        /// <returns>The corresponding <see cref="Manufacturer"/> entity, or null if <paramref name="model"/> is null.</returns>
        private static Manufacturer? MapToEntity(ManufacturerModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new Manufacturer(model.Id, model.ManufacturerName);
        }
    }
}
