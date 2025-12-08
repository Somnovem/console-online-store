namespace StoreDAL.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("manufacturers")]
public class Manufacturer : BaseEntity
{
    public Manufacturer()
        : base()
    {
    }

    public Manufacturer(int id, string manufacturerName)
        : base(id)
    {
        this.ManufacturerName = manufacturerName;
    }

    [Required]
    [Column("manufacturer_name")]
    [MaxLength(100)]
    public string ManufacturerName { get; set; }

    public virtual IList<Product> Products { get; set; } = new List<Product>();
}
