namespace StoreDAL.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("order_states")]
public class OrderState : BaseEntity
{
    public OrderState()
        : base()
    {
    }

    public OrderState(int id, string stateName)
        : base(id)
    {
        this.StateName = stateName;
    }

    [Required]
    [MaxLength(50)]
    [Column("state_name")]
    public string StateName { get; set; }

    public virtual IList<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
}
