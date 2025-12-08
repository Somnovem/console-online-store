namespace StoreBLL.Models
{
    /// <summary>
    /// Represents a user, including login credentials and role information.
    /// </summary>
    public class UserModel : AbstractModel
    {
        /// <summary>
        /// Creates a new instance of <see cref="UserModel"/> with specified values.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="userRoleId">The ID of the user's role.</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        /// <param name="login">The user's login name.</param>
        /// <param name="password">The user's password (assumed hashed).</param>
        /// <param name="roleName">The name of the user's role. Optional.</param>
        public UserModel(
            int id,
            int userRoleId,
            string firstName,
            string lastName,
            string login,
            string password,
            string? roleName = null)
            : base(id)
        {
            this.UserRoleId = userRoleId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Login = login;
            this.Password = password;
            this.RoleName = roleName;
        }

        /// <summary>
        /// Creates a new instance of <see cref="UserModel"/> with default values.
        /// String properties are initialized to empty to avoid null references.
        /// </summary>
        public UserModel()
            : base(0)
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Login = string.Empty;
            this.Password = string.Empty;
        }

        /// <summary>
        /// Gets the user's full name (first name followed by last name).
        /// </summary>
        public string UserName => $"{this.FirstName} {this.LastName}";

        /// <summary>
        /// Gets or sets the name of the user's role. Can be null.
        /// </summary>
        public string? RoleName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user's role.
        /// </summary>
        public int UserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user's login name.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the user's password (assumed hashed).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Returns a string representation of the user.
        /// </summary>
        /// <returns>A string containing the user's ID, full name, login, and role ID.</returns>
        public override string ToString()
        {
            return $"Id: {this.Id}, Name: {this.FirstName} {this.LastName}, Login: {this.Login}, RoleId: {this.UserRoleId}";
        }
    }
}
