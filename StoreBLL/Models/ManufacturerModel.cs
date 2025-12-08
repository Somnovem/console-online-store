namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a manufacturer in the business logic layer.
    /// </summary>
    public class ManufacturerModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="ManufacturerModel"/> with default values.
        /// The ID is set to 0 and the name is set to an empty string.
        /// This is useful for creating a blank or placeholder manufacturer.
        /// </summary>
        public ManufacturerModel()
            : base(0)
        {
            this.ManufacturerName = string.Empty;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ManufacturerModel"/> with a specified ID and name.
        /// </summary>
        /// <param name="id">The unique identifier of the manufacturer.</param>
        /// <param name="manufacturerName">The name of the manufacturer.</param>
        public ManufacturerModel(int id, string manufacturerName)
            : base(id)
        {
            this.ManufacturerName = manufacturerName;
        }

        /// <summary>
        /// Gets or sets the name of the manufacturer.
        /// </summary>
        public string ManufacturerName { get; set; }

        /// <summary>
        /// Returns the name of the manufacturer as the string representation of the object.
        /// </summary>
        /// <returns>A string containing the manufacturer's name.</returns>
        public override string ToString()
        {
            return this.ManufacturerName;
        }
    }
}
