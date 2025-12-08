using ConsoleApp.Helpers;
using ConsoleApp.MenuBuilder.Guest;
using ConsoleApp.MenuBuilder.User;
using ConsoleApp.MenuCore;
using ConsoleMenu.Builder;
using StoreBLL.Exceptions;
using StoreBLL.Models;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;

namespace ConsoleApp.Controllers;

public static class UserMenuController
{
    private static readonly Dictionary<UserRoles, Menu> RolesToMenu;
    private static int userId;
    private static UserRoles userRole;
    private static StoreDbContext context;

    static UserMenuController()
    {
        userId = 0;
        userRole = UserRoles.Guest;
        RolesToMenu = new Dictionary<UserRoles, Menu>();
        var factory = new StoreDbFactory(new TestDataFactory());
        context = factory.CreateContext();
        RolesToMenu.Add(UserRoles.Guest, new GuestMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.RegistredCustomer, new UserMainMenu().Create(context));
        RolesToMenu.Add(UserRoles.Administrator, new AdminMainMenu().Create(context));
    }

    public static StoreDbContext Context
    {
        get { return context; }
    }

    public static void Login()
    {
        var login = ConsoleManipulationHelper.GetString("Login: ");
        var password = ConsoleManipulationHelper.GetString("Password: ");

        UserService userService = new UserService(context);
        try
        {
            (int, UserRoles) loggedInUser = userService.Login(login, password);
            userId = loggedInUser.Item1;
            userRole = loggedInUser.Item2;

            Console.WriteLine("You were successfully logged in!");
            RolesToMenu[userRole].Run();
        }
        catch (UserNotFound e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void Register()
    {
        UserService userService = new UserService(context);

        var userModel = new UserModel(userService.GenerateNewId())
        {
            RoleId = 2,
        };

        userModel.Login = ConsoleManipulationHelper.GetLogin(userService);
        userModel.Password = ConsoleManipulationHelper.GetString("Password: ");
        userModel.Name = ConsoleManipulationHelper.GetString("First Name: ");
        userModel.LastName = ConsoleManipulationHelper.GetString("Last Name: ");

        userService.Add(userModel);
        Console.WriteLine("You were successfully registered!");

        userId = userModel.Id;
        userRole = UserRoles.RegistredCustomer;
        RolesToMenu[userRole].Run();
    }

    public static void Logout()
    {
        userId = 0;
        userRole = UserRoles.Guest;
        RolesToMenu[userRole].Run();
    }

    public static void Start()
    {
        RolesToMenu[userRole].Run();
    }
}