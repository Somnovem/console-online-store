namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a product's title, description, and associated category.
    /// </summary>
    public class ProductTitleModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProductTitleModel"/> with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the product title.</param>
        /// <param name="title">The title of the product.</param>
        /// <param name="categoryId">The ID of the category to which the product belongs.</param>
        public ProductTitleModel(int id, string title, int categoryId)
            : base(id)
        {
            this.Title = title;
            this.CategoryId = categoryId;
            this.Description = string.Empty; // Initialize Description to avoid null.
        }

        /// <summary>
        /// Creates a new instance of <see cref="ProductTitleModel"/> with default values.
        /// String properties are initialized to empty to prevent null values.
        /// </summary>
        public ProductTitleModel()
            : base(0)
        {
            this.Title = string.Empty;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a detailed description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product's category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Returns a string representation of the product title.
        /// </summary>
        /// <returns>A string containing the product's ID, title, and category ID.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Title: {this.Title}, CategoryId: {this.CategoryId}";
        }
    }
}
