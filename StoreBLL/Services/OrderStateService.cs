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
    /// Provides CRUD operations for managing order states.
    /// Implements <see cref="ICrud"/> interface.
    /// </summary>
    public class OrderStateService : ICrud
    {
        private readonly IOrderStateRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStateService"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing order state data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="repository"/> is null.</exception>
        public OrderStateService(IOrderStateRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not OrderStateModel orderStateModel)
            {
                throw new ArgumentException("Model is not an OrderStateModel.", nameof(model));
            }

            var entity = MapToEntity(new OrderStateModel(0, orderStateModel.StateName));
            this.repository.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.repository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.repository
                .GetAll()
                .Select(MapToModel)
                .OfType<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.repository.GetById(id)
                ?? throw new InvalidOperationException($"Order state with ID {id} not found.");

            return MapToModel(entity);
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not OrderStateModel orderStateModel)
            {
                throw new ArgumentException("Model is not an OrderStateModel.", nameof(model));
            }

            var entity = MapToEntity(orderStateModel);
            this.repository.Update(entity);
        }

        /// <summary>
        /// Maps an <see cref="OrderState"/> entity to an <see cref="OrderStateModel"/>.
        /// </summary>
        /// <param name="entity">The entity to map.</param>
        /// <returns>The mapped <see cref="OrderStateModel"/>.</returns>
        private static OrderStateModel MapToModel(OrderState entity)
        {
            return new OrderStateModel(entity.Id, entity.StateName);
        }

        /// <summary>
        /// Maps an <see cref="OrderStateModel"/> to an <see cref="OrderState"/> entity.
        /// </summary>
        /// <param name="model">The model to map.</param>
        /// <returns>The mapped <see cref="OrderState"/> entity.</returns>
        private static OrderState MapToEntity(OrderStateModel model)
        {
            return new OrderState(model.Id, model.StateName);
        }
    }
}
