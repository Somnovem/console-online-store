using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data.InitDataFactory;

namespace StoreDAL.Data
{
    public class StoreDbFactory
    {
        private readonly IDataFactory factory;

        public StoreDbFactory(IDataFactory factory)
        {
              this.factory = factory;
        }

        public static DbContextOptions<StoreDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<StoreDbContext>()
                .UseSqlite(CreateConnectionString())
                .Options;
        }

        public StoreDbContext CreateContext()
        {
            var context = new StoreDbContext(CreateOptions(), this.factory);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        private static string CreateConnectionString()
        {
            var dbPath = "store.db";
            var conString = new SqliteConnectionStringBuilder { DataSource = dbPath, Mode = SqliteOpenMode.ReadWriteCreate }.ConnectionString;
            return conString;
        }
    }
}
