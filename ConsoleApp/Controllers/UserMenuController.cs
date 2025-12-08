namespace ConsoleApp.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Helpers;
using StoreBLL.Services;
using StoreBLL.Models;
using ConsoleApp.MenuBuilder.Admin;
using ConsoleApp.MenuBuilder.Guest;
using ConsoleApp.MenuBuilder.User;

public class UserMenuController
{
    private readonly UserService userService;
    private readonly AdminMainMenu adminMainMenu;
    private readonly GuestMainMenu guestMainMenu;
    private readonly UserMainMenu userMainMenu;

    private UserModel? currentUser;
    private bool isExiting;

    public UserMenuController(
        UserService userService,
        AdminMainMenu adminMainMenu,
        GuestMainMenu guestMainMenu,
        UserMainMenu userMainMenu)
    {
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.adminMainMenu = adminMainMenu ?? throw new ArgumentNullException(nameof(adminMainMenu));
        this.guestMainMenu = guestMainMenu ?? throw new ArgumentNullException(nameof(guestMainMenu));
        this.userMainMenu = userMainMenu ?? throw new ArgumentNullException(nameof(userMainMenu));
    }

    public void Start()
    {
        ClearConsole();

        Console.WriteLine("Welcome to the Online Store Console App!");
        Console.WriteLine("---------------------------------------");
        this.Run();
    }

    public void Logout()
    {
        Console.WriteLine("\nLogging out...");
        this.currentUser = null;
        Console.WriteLine("You have been logged out.");
        InputHelper.PressAnyKeyToContinue();
    }

    public void Login()
    {
        Console.WriteLine("\n--- User Login ---");
        Console.Write("Enter Username: ");

        string username = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        try
        {
            this.currentUser = this.userService.Login(username, password);

            if (this.currentUser != null)
            {
                Console.WriteLine($"\nWelcome, {this.currentUser.UserName}! You are logged in as {this.currentUser.RoleName}.");
                InputHelper.PressAnyKeyToContinue();
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
                InputHelper.PressAnyKeyToContinue();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            InputHelper.PressAnyKeyToContinue();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"An error occurred during login: {ex.Message}");
            InputHelper.PressAnyKeyToContinue();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during login: {ex.Message}");
            throw;
        }
    }

    public void Exit()
    {
        this.isExiting = true;
    }

    private static void DisplayError(Exception ex)
    {
        ClearConsole();
        Console.WriteLine("An error occurred while building the menu!");
        Console.WriteLine($"Error Message: {ex.Message}");
        Console.WriteLine($"Error Details: {ex.ToString()}");
        InputHelper.PressAnyKeyToContinue();
    }

    private static void ClearConsole()
    {
        try
        {
            Console.Clear();
        }
        catch (System.IO.IOException)
        {
            Console.WriteLine();
        }
    }

    private void Run()
    {
        while (!this.isExiting)
        {
            try
            {
                ClearConsole();
                var menuItemsList = this.GetMenuItemsForCurrentUser();

                menuItemsList.Add((ConsoleKey.Escape, "Or press <Esc> to return", (Action)this.Exit));
                var menuItemsArray = menuItemsList.ToArray();

                foreach (var item in menuItemsArray.Where(i => i.action != null))
                {
                    Console.WriteLine($"<{item.id}>:  {item.caption}");
                }

                ConsoleKeyInfo res = Console.ReadKey(true);
                var selectedItem = menuItemsArray.FirstOrDefault(item => item.id == res.Key);

                selectedItem.action?.Invoke();
            }
            catch (ArgumentException ex)
            {
                DisplayError(ex);
            }
            catch (InvalidOperationException ex)
            {
                DisplayError(ex);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
                throw;
            }
        }
    }

    private List<(ConsoleKey id, string caption, Action action)> GetMenuItemsForCurrentUser()
    {
        List<(ConsoleKey id, string caption, Action action)> menuItemsList;
        string? userRole = this.currentUser?.RoleName;

        switch (userRole)
        {
            case "Admin":
                menuItemsList = this.adminMainMenu.GetMenuItems().ToList();
                var adminLogoutItem = menuItemsList.FirstOrDefault(item => item.id == ConsoleKey.F1);
                if (adminLogoutItem.id != default)
                {
                    menuItemsList.Remove(adminLogoutItem);
                    menuItemsList.Insert(0, (adminLogoutItem.id, "Logout", (Action)this.Logout));
                }

                break;
            case "Registered":
                menuItemsList = this.userMainMenu.GetMenuItems().ToList();
                var userLogoutItem = menuItemsList.FirstOrDefault(item => item.id == ConsoleKey.F1);
                if (userLogoutItem.id != default)
                {
                    menuItemsList.Remove(userLogoutItem);
                    menuItemsList.Insert(0, (userLogoutItem.id, "Logout", (Action)this.Logout));
                }

                break;
            default: // Guest or other roles
                menuItemsList = this.guestMainMenu.GetMenuItems().ToList();
                var guestLoginItem = menuItemsList.FirstOrDefault(item => item.id == ConsoleKey.F1);
                if (guestLoginItem.id != default)
                {
                    menuItemsList.Remove(guestLoginItem);
                    menuItemsList.Insert(0, (guestLoginItem.id, guestLoginItem.caption, (Action)this.Login));
                }

                break;
        }

        return menuItemsList;
    }
}
