using StoreDAL.Entities;

namespace StoreBLL.Models;

/// <summary>
/// Represents a user in the business logic layer.
/// </summary>
public class UserModel : AbstractModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserModel"/> class.
    /// Initialized user with just id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    public UserModel(int id)
        : base(id)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserModel"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="name">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="login">The login username of the user.</param>
    /// <param name="password">The password of the user (should be hashed in BLL).</param>
    /// <param name="roleId">The ID of the role assigned to the user.</param>
    public UserModel(int id, string name, string lastName, string login, string? password, int roleId)
        : base(id)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.RoleId = roleId;
    }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the login username of the user.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Gets or sets the password of the user. It is recommended to store a hashed password.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the ID of the role assigned to the user.
    /// </summary>
    public int RoleId { get; set; }
}