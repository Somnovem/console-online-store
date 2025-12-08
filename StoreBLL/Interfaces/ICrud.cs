namespace StoreBLL.Interfaces;
using System.Collections.Generic;
using StoreBLL.Models;

/// <summary>
/// Defines basic create, read, update, and delete operations for models.
/// </summary>
public interface ICrud
{
    /// <summary>
    /// Retrieves every model in the collection.
    /// </summary>
    /// <returns>A sequence of all available models.</returns>
    IEnumerable<AbstractModel> GetAll();

    /// <summary>
    /// Retrieves a single model based on its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the model.</param>
    /// <returns>The matching model, or null if none exists.</returns>
    AbstractModel? GetById(int id);

    /// <summary>
    /// Inserts a new model into the collection.
    /// </summary>
    /// <param name="model">The model instance to add.</param>
    void Add(AbstractModel model);

    /// <summary>
    /// Applies changes to an existing model.
    /// </summary>
    /// <param name="model">The updated model instance.</param>
    void Update(AbstractModel model);

    /// <summary>
    /// Removes a model using its ID.
    /// </summary>
    /// <param name="modelId">The ID of the model to remove.</param>
    void Delete(int modelId);
}