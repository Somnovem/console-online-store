namespace StoreDAL.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("customer_order_details")]
public class OrderDetail : BaseEntity
{
    public OrderDetail()
        : base()
    {
    }

    public OrderDetail(int id, int customerOrderId, int productId, decimal price, int productAmount)
        : base(id)
    {
        this.CustomerOrderId = customerOrderId;
        this.ProductId = productId;
        this.Price = price;
        this.ProductAmount = productAmount;
    }

    [Column("customer_order_id")]
    public int CustomerOrderId { get; set; }

    [ForeignKey("CustomerOrderId")]
    public CustomerOrder CustomerOrder { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [Required]
    [Column("price", TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Required]
    [Column("product_amount")]
    public int ProductAmount { get; set; }
}
