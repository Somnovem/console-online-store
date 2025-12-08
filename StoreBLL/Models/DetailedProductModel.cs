namespace StoreBLL.Models;

/// <summary>
/// Represents a detailed product in the system with references to its title and manufacturer,
/// as well as price and description.
/// </summary>
public class DetailedProductModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DetailedProductModel"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <param name="productTitleModel">The product title.</param>
    /// <param name="manufacturerModel">The product manufacturer.</param>
    /// <param name="description">A textual description of the product.</param>
    /// <param name="price">The unit price of the product.</param>
    public DetailedProductModel(int id, ProductTitleModel productTitleModel, ManufacturerModel manufacturerModel, string description, decimal price)
        : base(id)
    {
        this.ProductTitleModel = productTitleModel;
        this.ManufacturerModel = manufacturerModel;
        this.Description = description;
        this.UnitPrice = price;
    }

    /// <summary>
    /// Gets or sets the ID of the product title.
    /// </summary>
    public ProductTitleModel ProductTitleModel { get; set; }

    /// <summary>
    /// Gets or sets the ID of the manufacturer of the product.
    /// </summary>
    public ManufacturerModel ManufacturerModel { get; set; }

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
        return $"{this.Id} | {this.ProductTitleModel.Title} | {this.ManufacturerModel.Name} | {this.UnitPrice} | {this.Description}";
    }
}