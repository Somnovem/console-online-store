using System.ComponentModel.DataAnnotations.Schema;

namespace StoreDAL.Entities;
using System.Collections.Generic;

[Table("users")]
public class User : BaseEntity
{
    public User()
    {
    }

    public User(int id, string name, string lastName, string login, string password, int roleId)
        : base(id)
    {
        this.Name = name;
        this.LastName = lastName;
        this.Login = login;
        this.Password = password;
        this.RoleId = roleId;
    }

    [Column("first_name", TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Column("last_name", TypeName = "varchar(100)")]
    public string LastName { get; set; }

    [Column("login", TypeName = "varchar(100)")]
    public string Login { get; set; }

    [Column("password", TypeName = "varchar(100)")]
    public string Password { get; set; }

    [Column("user_role_id")]
    public int RoleId { get; set; }

    public UserRole Role { get; set; }

    public virtual IList<CustomerOrder> Order { get; set; }
}
