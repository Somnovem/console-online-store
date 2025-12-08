namespace StoreDAL.Entities;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        this.UserRoleName = roleName;
    }

    [Required]
    [Column("user_role_name")]

    public string UserRoleName { get; set; }

    public virtual IList<User> Users { get; set; } = new List<User>();
}
