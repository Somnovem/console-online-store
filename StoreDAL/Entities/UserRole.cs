namespace StoreDAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user_roles")]
public class UserRole : BaseEntity
{
    public UserRole()
    {
    }

    public UserRole(int id, string roleName)
        : base(id)
    {
        this.RoleName = roleName;
    }

    [Column("user_role_name", TypeName = "varchar(30)")]
    public string RoleName { get; set; }

    public virtual IList<User> User { get; set; }
}
