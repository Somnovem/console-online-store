using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;

[Table("manufacturers")]
public class Manufacturer : BaseEntity
{
    public Manufacturer()
    {
    }

    public Manufacturer(int id, string name)
        : base(id)
    {
        this.Name = name;
    }

    [Column("manufacturer_name", TypeName = "varchar(100)")]
    public string Name { get; set; }
}
