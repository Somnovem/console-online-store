namespace StoreBLL.Models;
public abstract class AbstractModel
{
    protected AbstractModel(int id)
    {
        this.Id = id;
    }

    public int Id { get; set; }
}
