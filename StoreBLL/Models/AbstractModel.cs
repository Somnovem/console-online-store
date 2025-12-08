namespace StoreBLL.Models;

/// <summary>
/// Serves as the base class for data models that require a unique identifier.
/// This class is abstract and cannot be instantiated directly.
/// </summary>
public abstract class AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractModel"/> class.
    /// The constructor is protected to ensure derived classes supply an ID.
    /// </summary>
    /// <param name="id">The model’s unique identifier.</param>
    protected AbstractModel(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets or sets the model’s unique identifier.
    /// </summary>
    public int Id { get; set; }
}
