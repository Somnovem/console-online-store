namespace StoreBLL.Models
{
    /// <summary>
    /// Represents the details of a customer order, including product information and quantity.
    /// </summary>
    public class OrderDetailModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrderDetailModel"/> with default values.
        /// </summary>
        public OrderDetailModel()
            : base(0)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="OrderDetailModel"/> with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the order detail.</param>
        /// <param name="customerOrderId">The ID of the associated customer order.</param>
        /// <param name="productId">The ID of the ordered product.</param>
        /// <param name="productAmount">The quantity of the product ordered.</param>
        /// <param name="price">The price of a single unit of the product.</param>
        public OrderDetailModel(int id, int customerOrderId, int productId, int productAmount, decimal price)
            : base(id)
        {
            this.CustomerOrderId = customerOrderId;
            this.ProductId = productId;
            this.ProductAmount = productAmount;
            this.Price = price;
        }

        /// <summary>
        /// Gets or sets the ID of the associated customer order.
        /// </summary>
        public int CustomerOrderId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the ordered product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product ordered.
        /// </summary>
        public int ProductAmount { get; set; }

        /// <summary>
        /// Gets or sets the price of a single unit of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Returns a string representation of the order detail.
        /// </summary>
        /// <returns>A string containing the detail's ID, order ID, product ID, quantity, and price.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, OrderId: {this.CustomerOrderId}, ProductId: {this.ProductId}, ProductAmount: {this.ProductAmount}, Price: {this.Price:C}";
        }
    }
}
