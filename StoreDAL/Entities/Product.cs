using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;
using System.Collections.Generic;

[Table("products")]
public class Product : BaseEntity
{
    public Product()
    {
    }

    public Product(int id, int titleId, int manufacturerId, string description, decimal price)
        : base(id)
    {
        this.TitleId = titleId;
        this.ManufacturerId = manufacturerId;
        this.Description = description;
        this.UnitPrice = price;
    }

    [Column("product_title_id")]
    public int TitleId { get; set; }

    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

    [Column("unit_price")]
    public decimal UnitPrice { get; set; }

    [Column("comment", TypeName = "varchar(255)")]
    public string Description { get; set; }

    public ProductTitle Title { get; set; }

    public Manufacturer Manufacturer { get; set; }

    public virtual IList<OrderDetail> OrderDetails { get; set; }
}