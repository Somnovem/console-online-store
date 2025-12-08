namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a user role, including its unique ID and name.
    /// </summary>
    public class UserRoleModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="UserRoleModel"/> with a specified ID and role name.
        /// </summary>
        /// <param name="id">The unique identifier of the user role.</param>
        /// <param name="userRoleName">The name of the user role.</param>
        public UserRoleModel(int id, string userRoleName)
            : base(id)
        {
            this.UserRoleName = userRoleName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="UserRoleModel"/> with default values.
        /// The role name is initialized to an empty string to avoid null reference warnings.
        /// </summary>
        public UserRoleModel()
            : base(0)
        {
            this.UserRoleName = string.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the user role.
        /// </summary>
        public string UserRoleName { get; set; }

        /// <summary>
        /// Returns a string representation of the user role.
        /// </summary>
        /// <returns>A string containing the role's ID and name.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, RoleName: {this.UserRoleName}";
        }
    }
}
