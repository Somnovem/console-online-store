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
    /// Provides CRUD operations for managing order details.
    /// Implements <see cref="ICrud"/> interface.
    /// </summary>
    public class OrderDetailService : ICrud
    {
        private readonly IOrderDetailRepository orderDetailRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailService"/> class.
        /// </summary>
        /// <param name="orderDetailRepository">The repository for accessing order detail data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="orderDetailRepository"/> is null.</exception>
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            this.orderDetailRepository = orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not OrderDetailModel orderDetailModel)
            {
                throw new ArgumentException("Model is not an OrderDetailModel.", nameof(model));
            }

            var orderDetailEntity = MapToEntity(orderDetailModel);
            this.orderDetailRepository.Add(orderDetailEntity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.orderDetailRepository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.orderDetailRepository.GetAll()
                .Select(MapToModel)
                .Cast<AbstractModel>()
                .ToList();
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.orderDetailRepository.GetById(id)
                ?? throw new InvalidOperationException($"Order detail with ID {id} not found.");

            return MapToModel(entity);
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not OrderDetailModel orderDetailModel)
            {
                throw new ArgumentException("Model is not an OrderDetailModel.", nameof(model));
            }

            var orderDetailEntity = MapToEntity(orderDetailModel);
            this.orderDetailRepository.Update(orderDetailEntity);
        }

        /// <summary>
        /// Converts an <see cref="OrderDetail"/> entity to an <see cref="OrderDetailModel"/>.
        /// </summary>
        /// <param name="entity">The entity to map.</param>
        /// <returns>The mapped <see cref="OrderDetailModel"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null.</exception>
        private static OrderDetailModel MapToModel(OrderDetail? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new OrderDetailModel(
                entity.Id,
                entity.CustomerOrderId,
                entity.ProductId,
                entity.ProductAmount,
                entity.Price);
        }

        /// <summary>
        /// Converts an <see cref="OrderDetailModel"/> to an <see cref="OrderDetail"/> entity.
        /// </summary>
        /// <param name="model">The model to map.</param>
        /// <returns>The mapped <see cref="OrderDetail"/> entity.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="model"/> is null.</exception>
        private static OrderDetail MapToEntity(OrderDetailModel? model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return new OrderDetail(
                model.Id,
                model.CustomerOrderId,
                model.ProductId,
                model.Price,
                model.ProductAmount);
        }
    }
}
