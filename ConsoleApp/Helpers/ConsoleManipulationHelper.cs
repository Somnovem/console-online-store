using StoreBLL.Services;

namespace ConsoleApp.Helpers;

public static class ConsoleManipulationHelper
{
    public static string GetLogin(UserService userService)
    {
        string result = string.Empty;
        bool stillDo = false;
        do
        {
            Console.WriteLine("Login: ");
            var login = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(login) || !userService.CheckIfLoginValid(login))
            {
                stillDo = true;
                Console.WriteLine("Such login already exists");
            }
            else
            {
                stillDo = false;
                result = login;
            }
        }
        while (stillDo);

        return result;
    }

    public static string GetString(string message)
    {
        string result = string.Empty;
        bool stillDo = false;
        do
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                stillDo = true;
            }
            else
            {
                stillDo = false;
                result = input;
            }
        }
        while (stillDo);

        return result;
    }
}