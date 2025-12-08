namespace StoreBLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a customer order, including its details and total amount calculation.
    /// </summary>
    public class CustomerOrderModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="CustomerOrderModel"/> with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the order.</param>
        /// <param name="operationTime">The date and time when the order was placed or processed.</param>
        /// <param name="customerId">The identifier of the customer who placed the order.</param>
        /// <param name="orderStateId">The identifier representing the current state of the order.</param>
        public CustomerOrderModel(int id, DateTime operationTime, int customerId, int orderStateId)
            : base(id)
        {
            this.OperationTime = operationTime;
            this.CustomerId = customerId;
            this.OrderStateId = orderStateId;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CustomerOrderModel"/> with default values.
        /// </summary>
        public CustomerOrderModel()
            : base(0)
        {
        }

        /// <summary>
        /// Gets or sets the date and time when the order was placed or processed.
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the current state of the order.
        /// </summary>
        public int OrderStateId { get; set; }

        /// <summary>
        /// Gets the total amount of the order, calculated by summing the product of price and quantity for each order detail.
        /// </summary>
        public decimal TotalAmount => this.OrderDetails.Sum(detail => detail.Price * detail.ProductAmount);

        /// <summary>
        /// Gets or sets the collection of order detail models associated with this order.
        /// </summary>
        public ICollection<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();

        /// <summary>
        /// Returns a string representation of the customer order.
        /// </summary>
        /// <returns>A string containing the order ID, operation date, customer ID, and order state ID.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Date: {this.OperationTime.ToShortDateString()}, CustomerId: {this.CustomerId}, StateId: {this.OrderStateId}";
        }
    }
}
