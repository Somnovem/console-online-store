namespace ConsoleApp.Controllers;

using System;
using StoreBLL.Models;
using StoreBLL.Services;
using ConsoleApp.Helpers;
using System.Linq;

public class UserController
{
    private readonly UserService userService;
    private readonly UserRoleService userRoleService;

    public UserController(
        UserService userService,
        UserRoleService userRoleService)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.userRoleService = userRoleService ?? throw new ArgumentNullException(nameof(userRoleService));
    }

    public void AddUser()
    {
        Console.WriteLine("\n--- Register New User ---");
        try
        {
            string firstName = InputHelper.ReadString("First Name");
            string lastName = InputHelper.ReadString("Last Name");
            string login = InputHelper.ReadString("Login (e.g., a username)");
            string password = InputHelper.ReadString("Password");
            int roleId = InputHelper.ReadInt("User Role ID (e.g., 1 for Admin, 2 for Registered, 3 for Guest)");
            var newUser = new UserModel(0, roleId, firstName, lastName, login, password);
            this.userService.Add(newUser);
            Console.WriteLine("User registered successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error registering user: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateUser()
    {
        Console.WriteLine("\n--- Update User ---");
        try
        {
            int id = InputHelper.ReadInt("User ID to update");
            var existingUser = this.userService.GetById(id) as UserModel;

            if (existingUser == null)
            {
                Console.WriteLine($"User with ID {id} not found.");
                return;
            }

            var updatedUser = new UserModel(
                id,
                InputHelper.ReadInt($"New User Role ID (current: {existingUser.UserRoleId})"),
                InputHelper.ReadString($"New First Name (current: {existingUser.FirstName})"),
                InputHelper.ReadString($"New Last Name (current: {existingUser.LastName})"),
                InputHelper.ReadString($"New Login (current: {existingUser.Login})"),
                InputHelper.ReadString($"New Password (current: *****)"));
            this.userService.Update(updatedUser);
            Console.WriteLine($"User with ID {id} updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteUser()
    {
        Console.WriteLine("\n--- Delete User ---");
        try
        {
            int id = InputHelper.ReadInt("User ID to delete");
            this.userService.Delete(id);
            Console.WriteLine($"User with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowUser()
    {
        Console.WriteLine("\n--- Show User Details ---");
        try
        {
            int id = InputHelper.ReadInt("User ID to view");
            var user = this.userService.GetById(id) as UserModel;
            if (user != null)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName} {user.LastName}, Login: {user.Login}, RoleId: {user.UserRoleId}, Role Name: {user.RoleName}");
            }
            else
            {
                Console.WriteLine($"User with ID {id} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing user: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("\n--- All Users ---");
        try
        {
            var users = this.userService.GetAll().OfType<UserModel>().ToList();
            if (users.Count != 0)
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"Id: {user.Id}, Name: {user.FirstName} {user.LastName}, Login: {user.Login}, RoleId: {user.UserRoleId}, Role Name: {user.RoleName}");
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all users: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllUserRoles()
    {
        Console.WriteLine("\n--- All User Roles ---");
        try
        {
            var userRoles = this.userRoleService.GetAll().ToList();
            if (userRoles.Count != 0)
            {
                foreach (var role in userRoles)
                {
                    Console.WriteLine(role.ToString());
                }
            }
            else
            {
                Console.WriteLine("No user roles found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error showing all user roles: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void AddUserRole()
    {
        Console.WriteLine("\n--- Add New User Role ---");
        try
        {
            var userRole = InputHelper.ReadUserRoleModel();
            this.userRoleService.Add(userRole);
            Console.WriteLine("User role added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding user role: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateUserRole()
    {
        Console.WriteLine("\n--- Update User Role ---");
        try
        {
            int id = InputHelper.ReadInt("User Role ID to update");
            var existingRole = this.userRoleService.GetById(id);
            if (existingRole == null)
            {
                Console.WriteLine($"User Role with ID {id} not found.");
                return;
            }

            var updatedRole = InputHelper.ReadUserRoleModel();
            updatedRole.Id = id;
            this.userRoleService.Update(updatedRole);
            Console.WriteLine($"User Role with ID {id} updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user role: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteUserRole()
    {
        Console.WriteLine("\n--- Delete User Role ---");
        try
        {
            int id = InputHelper.ReadInt("User Role ID to delete");
            this.userRoleService.Delete(id);
            Console.WriteLine($"User Role with ID {id} deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting user role: {ex.Message}");
        }

        InputHelper.PressAnyKeyToContinue();
    }
}
