namespace StoreBLL.Models;

/// <summary>
/// Represents a product in the system with references to its price and description.
/// </summary>
public class ProductModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductModel"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="titleId">the ID of the product title.</param>
    /// <param name="manufacturerId">Gets or sets the ID of the manufacturer of the product.</param>
    /// <param name="description">A textual description of the product.</param>
    /// <param name="price">The unit price of the product.</param>
    public ProductModel(int id, int titleId, int manufacturerId, string description, decimal price)
        : base(id)
    {
        this.TitleId = titleId;
        this.ManufacturerId = manufacturerId;
        this.Description = description;
        this.UnitPrice = price;
    }

    /// <summary>
    /// Gets or sets the ID of the product title.
    /// </summary>
    public int TitleId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the manufacturer of the product.
    /// </summary>
    public int ManufacturerId { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the textual description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{this.Id} | {this.UnitPrice} | {this.Description}";
    }
}