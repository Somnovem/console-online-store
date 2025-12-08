namespace StoreDAL.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("categories")]
public class Category : BaseEntity
{
    public Category()
        : base()
    {
    }

    public Category(int id, string categoryName)
        : base(id)
    {
        this.CategoryName = categoryName;
    }

    [Required]
    [Column("category_name")]
    [MaxLength(100)]
    public string CategoryName { get; set; }

    public virtual IList<ProductTitle> ProductTitles { get; set; } = new List<ProductTitle>();
}
