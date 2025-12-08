using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;

[Table("users")]
public class User : BaseEntity
{
    public User()
        : base()
    {
    }

    public User(int id, string firstName, string lastName, string login, string password, int userRoleId)
        : base(id)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.UserRoleId = userRoleId;
    }

    [Required]
    [Column("first_name")]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name")]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [Column("login")]
    [MaxLength(50)]
    public string Login { get; set; }

    [Required]
    [Column("Password")]
    [MaxLength(255)]
    public string Password { get; set; }

    [Column("user_role_id")]
    public int UserRoleId { get; set; }

    [ForeignKey("UserRoleId")]
    public UserRole UserRole { get; set; }

    public virtual IList<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
}
