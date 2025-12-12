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
    /// Provides CRUD operations for managing customer orders.
    /// Implements <see cref="ICrud"/> interface.
    /// </summary>
    public class CustomerOrderService : ICrud
    {
        private readonly ICustomerOrderRepository customerOrderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderService"/> class.
        /// </summary>
        /// <param name="customerOrderRepository">Repository used for accessing customer order data.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="customerOrderRepository"/> is null.</exception>
        public CustomerOrderService(ICustomerOrderRepository customerOrderRepository)
        {
            this.customerOrderRepository = customerOrderRepository ?? throw new ArgumentNullException(nameof(customerOrderRepository));
        }

        /// <inheritdoc/>
        public void Add(AbstractModel model)
        {
            if (model is not CustomerOrderModel customerOrderModel)
            {
                throw new ArgumentException("Model is not a CustomerOrderModel.", nameof(model));
            }

            var entity = MapToEntity(new CustomerOrderModel(
                0,
                customerOrderModel.OperationTime,
                customerOrderModel.CustomerId,
                customerOrderModel.OrderStateId))
                ?? throw new InvalidOperationException("Mapping to entity failed.");

            this.customerOrderRepository.Add(entity);
        }

        /// <inheritdoc/>
        public void Delete(int modelId)
        {
            this.customerOrderRepository.DeleteById(modelId);
        }

        /// <inheritdoc/>
        public IEnumerable<AbstractModel> GetAll()
        {
            return this.customerOrderRepository.GetAll()
                .Select(MapToModel)
                .Where(m => m != null) !
                .ToList() !;
        }

        /// <inheritdoc/>
        public AbstractModel GetById(int id)
        {
            var entity = this.customerOrderRepository.GetById(id)
                ?? throw new InvalidOperationException($"Customer order with ID {id} not found.");

            return MapToModel(entity) !;
        }

        /// <inheritdoc/>
        public void Update(AbstractModel model)
        {
            if (model is not CustomerOrderModel customerOrderModel)
            {
                throw new ArgumentException("Model is not a CustomerOrderModel.", nameof(model));
            }

            var entity = MapToEntity(customerOrderModel)
                ?? throw new InvalidOperationException("Mapping to entity failed.");

            this.customerOrderRepository.Update(entity);
        }

        /// <summary>
        /// Maps a <see cref="CustomerOrder"/> entity to a <see cref="CustomerOrderModel"/>.
        /// </summary>
        /// <param name="entity">The customer order entity to map.</param>
        /// <returns>The corresponding <see cref="CustomerOrderModel"/>, or null if <paramref name="entity"/> is null.</returns>
        private static CustomerOrderModel? MapToModel(CustomerOrder? entity)
        {
            if (entity == null)
            {
                return null;
            }

            var model = new CustomerOrderModel(entity.Id, entity.OperationTime, entity.CustomerId, entity.OrderStateId);
            model.OrderStateName = entity.OrderState?.StateName;

            if (entity.CustomerOrderDetails != null)
            {
                foreach (var detailEntity in entity.CustomerOrderDetails)
                {
                    model.OrderDetails.Add(new OrderDetailModel(
                        detailEntity.Id,
                        detailEntity.CustomerOrderId,
                        detailEntity.ProductId,
                        detailEntity.ProductAmount,
                        detailEntity.Price));
                }
            }

            return model;
        }

        /// <summary>
        /// Maps a <see cref="CustomerOrderModel"/> to a <see cref="CustomerOrder"/> entity.
        /// </summary>
        /// <param name="model">The customer order model to map.</param>
        /// <returns>The corresponding <see cref="CustomerOrder"/> entity, or null if <paramref name="model"/> is null.</returns>
        private static CustomerOrder? MapToEntity(CustomerOrderModel? model)
        {
            if (model == null)
            {
                return null;
            }

            var entity = new CustomerOrder(model.Id, model.OperationTime, model.CustomerId, model.OrderStateId);

            if (model.OrderDetails != null)
            {
                foreach (var detailModel in model.OrderDetails)
                {
                    entity.CustomerOrderDetails.Add(new OrderDetail(
                        detailModel.Id,
                        detailModel.CustomerOrderId,
                        detailModel.ProductId,
                        detailModel.Price,
                        detailModel.ProductAmount));
                }
            }

            return entity;
        }
    }
}
