namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a product category in the store.
    /// </summary>
    public class CategoryModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="CategoryModel"/> with the specified values.
        /// </summary>
        /// <param name="id">The unique ID of the category.</param>
        /// <param name="categoryName">The name of the category.</param>
        public CategoryModel(int id, string categoryName)
            : base(id)
        {
            this.CategoryName = categoryName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CategoryModel"/> with default values.
        /// </summary>
        public CategoryModel()
            : base(0)
        {
            // Initialize the property to avoid CS8618 warning.
            this.CategoryName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the category's name.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Returns the name of the category as a string.
        /// </summary>
        /// <returns>The category name.</returns>
        public override string ToString()
        {
            return this.CategoryName;
        }
    }
}
