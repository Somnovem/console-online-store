using ConsoleApp.Controllers;
using ConsoleApp.Helpers;
using ConsoleApp.MenuBuilder.Admin;
using ConsoleApp.MenuBuilder.Guest;
using ConsoleApp.MenuBuilder.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreBLL.Services;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;
using StoreDAL.Interfaces;
using StoreDAL.Repository;

namespace ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<StoreDbContext>();
                    context.Database.Migrate();

                    var userMenuController = services.GetRequiredService<UserMenuController>();
                    userMenuController.Start();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Warning);
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.None);
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.None);
                    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Core Configuration
                    services.AddSingleton<IDataFactory, ReleaseDataFactory>();

                    ConfigureDatabase(services);
                    ConfigureRepositories(services);
                    ConfigureServices(services);
                    ConfigureControllersAndMenus(services);
                });

        /// <summary>
        /// Configures the database context.
        /// </summary>
        private static void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                string dbPath = Path.Combine(AppContext.BaseDirectory, "../..", "store.db");
                options.UseSqlite($"Data Source={dbPath}");
                options.EnableSensitiveDataLogging(false);
                options.LogTo(_ => { }, LogLevel.None);
            });
        }

        /// <summary>
        /// Configures Data Access Layer (DAL) repositories.
        /// </summary>
        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderStateRepository, OrderStateRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTitleRepository, ProductTitleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        }

        /// <summary>
        /// Configures Business Logic Layer (BLL) services.
        /// </summary>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CategoryService>();
            services.AddScoped<CustomerOrderService>();
            services.AddScoped<ManufacturerService>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<OrderStateService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductTitleService>();
            services.AddScoped<UserService>();
            services.AddScoped<UserRoleService>();
        }

        /// <summary>
        /// Configures Presentation Layer (Controllers and Menu Builders).
        /// </summary>
        private static void ConfigureControllersAndMenus(IServiceCollection services)
        {
            // Controllers
            services.AddScoped<UserController>();
            services.AddScoped<ProductController>();
            services.AddScoped<ShopController>();
            services.AddScoped<UserMenuController>(); // Main app controller

            // Menu Builders
            services.AddScoped<AdminMainMenu>();
            services.AddScoped<GuestMainMenu>();
            services.AddScoped<UserMainMenu>();
        }
    }
}
