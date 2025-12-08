namespace StoreDAL.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

[Table("customer_orders")]
public class CustomerOrder : BaseEntity
{
    public CustomerOrder()
        : base()
    {
    }

    public CustomerOrder(int id, DateTime operationTime, int customerId, int orderStateId)
        : base(id)
    {
        this.OperationTime = operationTime;
        this.CustomerId = customerId;
        this.OrderStateId = orderStateId;
    }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public User Customer { get; set; }

    [Required]
    [Column("operation_time")]
    public DateTime OperationTime { get; set; }

    [Column("order_state_id")]
    public int OrderStateId { get; set; }

    [ForeignKey("OrderStateId")]
    public OrderState OrderState { get; set; }

    public virtual IList<OrderDetail> CustomerOrderDetails { get; set; } = new List<OrderDetail>();

    [NotMapped]
    public decimal TotalAmount => this.CustomerOrderDetails.Sum(detail => detail.Price * detail.ProductAmount);
}
