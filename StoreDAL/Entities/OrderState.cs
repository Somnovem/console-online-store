using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;

[Table("order_states")]
public class OrderState : BaseEntity
{
    public OrderState()
    {
    }

    public OrderState(int id, string stateName)
        : base(id)
    {
        this.StateName = stateName;
    }

    [Column("state_name", TypeName = "varchar(100)")]
    public string StateName { get; set; }

    public virtual IList<CustomerOrder> Order { get; set; }
}
