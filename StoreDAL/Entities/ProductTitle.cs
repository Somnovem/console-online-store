namespace StoreDAL.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("product_titles")]
public class ProductTitle : BaseEntity
{
    public ProductTitle()
        : base()
    {
    }

    public ProductTitle(int id, string title, int categoryId)
        : base(id)
    {
        this.Title = title;
        this.CategoryId = categoryId;
    }

    [Required]
    [Column("product_title")]
    [MaxLength(255)]
    public string Title { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }

    public virtual IList<Product> Products { get; set; } = new List<Product>();
}
