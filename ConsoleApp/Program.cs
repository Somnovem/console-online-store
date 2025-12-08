using ConsoleApp.Controllers;
using ConsoleApp.Helpers;
using ConsoleApp.MenuBuilder.Admin;
using ConsoleApp.MenuBuilder.Guest;
using ConsoleApp.MenuBuilder.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Encountered an error: {ex.Message}");
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IDataFactory, ReleaseDataFactory>();

                    services.AddDbContext<StoreDbContext>(options =>
                    {
                        string dbPath = Path.Combine(AppContext.BaseDirectory, "../..", "store.db");
                        options.UseSqlite($"Data Source={dbPath}");
                    });

                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                    services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
                    services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
                    services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
                    services.AddScoped<IOrderStateRepository, OrderStateRepository>();
                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<IProductTitleRepository, ProductTitleRepository>();
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<IUserRoleRepository, UserRoleRepository>();

                    services.AddScoped<CategoryService>();
                    services.AddScoped<CustomerOrderService>();
                    services.AddScoped<ManufacturerService>();
                    services.AddScoped<OrderDetailService>();
                    services.AddScoped<OrderStateService>();
                    services.AddScoped<ProductService>();
                    services.AddScoped<ProductTitleService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<UserRoleService>();

                    services.AddScoped<UserController>();
                    services.AddScoped<ProductController>();
                    services.AddScoped<ShopController>();
                    services.AddScoped<UserMenuController>();

                    services.AddScoped<AdminMainMenu>();
                    services.AddScoped<GuestMainMenu>();
                    services.AddScoped<UserMainMenu>();
                });
    }
}
