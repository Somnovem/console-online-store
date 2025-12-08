namespace StoreDAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class BaseEntity(int id)
{
    protected BaseEntity()
        : this(0)
    {
    }

    [Column("id")]
    public int Id { get; set; } = id;
}
