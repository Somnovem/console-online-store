namespace StoreBLL.Models;

public class ProductTitleModel : AbstractModel
{
    public ProductTitleModel(int id, string title) : base(id)
    {
        this.Title = title;
    }

    public string Title { get; set; }
}