namespace StoreBLL.Models
{
    /// <summary>
    /// Represents the state of a customer order.
    /// </summary>
    public class OrderStateModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrderStateModel"/> with a specified ID and state name.
        /// </summary>
        /// <param name="id">The unique identifier of the order state.</param>
        /// <param name="stateName">The name of the order state.</param>
        public OrderStateModel(int id, string stateName)
            : base(id)
        {
            this.StateName = stateName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="OrderStateModel"/> with default values.
        /// The ID is set to 0 and the state name is set to an empty string.
        /// </summary>
        public OrderStateModel()
            : base(0)
        {
            this.StateName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the order state.
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// Returns a string representation of the order state.
        /// </summary>
        /// <returns>A string containing the state's ID and name.</returns>
        public override string ToString()
        {
            return $"Id:{this.Id} {this.StateName}";
        }
    }
}
