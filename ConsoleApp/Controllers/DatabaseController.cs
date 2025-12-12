namespace ConsoleApp.Controllers;

using System;
using ConsoleApp.Helpers;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;

/// <summary>
/// Controller for database administration operations.
/// </summary>
public class DatabaseController
{
    private readonly StoreDbContext context;
    private readonly DatabaseSeeder seeder;

    public DatabaseController(StoreDbContext context, DatabaseSeeder seeder)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
    }

    /// <summary>
    /// Drops the entire database and re-creates it with seed data.
    /// WARNING: This will delete all data in the database!
    /// </summary>
    public void DropDatabase()
    {
        Console.WriteLine("\n--- Drop Database ---");
        Console.WriteLine("WARNING: This operation will DELETE ALL DATA in the database!");
        Console.WriteLine("This action cannot be undone.");
        Console.Write("\nAre you sure you want to drop the database? Type 'YES' to confirm: ");

        string confirmation = Console.ReadLine() ?? string.Empty;

        if (confirmation.Trim().ToUpper() != "YES")
        {
            Console.WriteLine("Database drop cancelled.");
            InputHelper.PressAnyKeyToContinue();
            return;
        }

        try
        {
            Console.WriteLine("\nDropping database...");
            this.context.Database.EnsureDeleted();

            Console.WriteLine("Recreating database...");
            this.context.Database.Migrate();

            Console.WriteLine("Seeding database with initial data...");
            this.seeder.Seed();

            Console.WriteLine("\nDatabase has been successfully dropped and recreated with seed data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred while dropping the database: {ex.Message}");
            Console.WriteLine("The database may be in an inconsistent state. Please restart the application.");
        }

        InputHelper.PressAnyKeyToContinue();
    }
}
