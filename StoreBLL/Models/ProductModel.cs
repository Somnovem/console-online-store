namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a product, including pricing, availability, and manufacturer information.
    /// </summary>
    public class ProductModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProductModel"/> with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <param name="productTitleId">The ID of the product title.</param>
        /// <param name="manufacturerId">The ID of the manufacturer.</param>
        /// <param name="unitPrice">The price per unit of the product.</param>
        /// <param name="availableQuantity">The quantity of the product available in stock.</param>
        public ProductModel(int id, int productTitleId, int manufacturerId, decimal unitPrice, int availableQuantity)
            : base(id)
        {
            this.ProductTitleId = productTitleId;
            this.ManufacturerId = manufacturerId;
            this.UnitPrice = unitPrice;
            this.AvailableQuantity = availableQuantity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductModel"/> class.
        /// Creates a new instance of <see cref="ProductModel"/> with default values.
        /// </summary>
        public ProductModel()
            : base(0)
        {
        }

        /// <summary>
        /// Gets or sets the ID of the product title.
        /// </summary>
        public int ProductTitleId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the manufacturer.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the available stock quantity of the product.
        /// </summary>
        public int AvailableQuantity { get; set; }

        /// <summary>
        /// Returns a string representation of the product.
        /// </summary>
        /// <returns>A string containing the product's ID, title ID, manufacturer ID, unit price, and available quantity.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, TitleId: {this.ProductTitleId}, ManufacturerId: {this.ManufacturerId}, UnitPrice: {this.UnitPrice:C}, Qty: {this.AvailableQuantity}";
        }
    }
}
