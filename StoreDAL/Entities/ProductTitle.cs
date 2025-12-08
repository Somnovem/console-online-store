namespace StoreDAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("product_titles")]
public class ProductTitle : BaseEntity
{
    public ProductTitle()
    {
    }

    public ProductTitle(int id, string title, int categoryId)
        : base(id)
    {
        this.Title = title;
        this.CategoryId = categoryId;
    }

    [Column("product_title", TypeName = "varchar(100)")]
    public string Title { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public virtual IList<Product> Products { get; set; }
}
