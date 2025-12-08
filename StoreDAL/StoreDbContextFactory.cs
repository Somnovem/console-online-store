using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StoreDAL.Data;
using StoreDAL.Data.InitDataFactory;

namespace StoreDAL
{
    public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
    {
        public StoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionsBuilder.UseSqlite("Data Source=store.db");

            return new StoreDbContext(optionsBuilder.Options, new ReleaseDataFactory());
        }
    }
}
