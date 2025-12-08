using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;

[Table("products")]
public class Product : BaseEntity
{
    public Product()
        : base()
    {
    }

    public Product(int id, int productTitleId, int manufacturerId, string comment, decimal unitPrice, int availableQuantity)
        : base(id)
    {
        this.ProductTitleId = productTitleId;
        this.ManufacturerId = manufacturerId;
        this.Comment = comment;
        this.UnitPrice = unitPrice;
        this.AvailableQuantity = availableQuantity;
    }

    [Column("product_title_id")]
    public int ProductTitleId { get; set; }

    [ForeignKey("ProductTitleId")]
    public ProductTitle ProductTitle { get; set; }

    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

    [ForeignKey("ManufacturerId")]
    public Manufacturer Manufacturer { get; set; }

    [Required]
    [Column("unit_price", TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }

    [MaxLength(1000)]
    [Column("comment")]
    public string Comment { get; set; }

    [Required]
    [Column("available_quantity")]
    public int AvailableQuantity { get; set; }

    public virtual IList<OrderDetail> CustomerOrderDetails { get; set; } = new List<OrderDetail>();
}
