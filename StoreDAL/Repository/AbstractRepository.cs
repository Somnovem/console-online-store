using StoreDAL.Data;

namespace StoreDAL.Repository
{
    public abstract class AbstractRepository
    {
        protected readonly StoreDbContext context;

        protected AbstractRepository(StoreDbContext context)
        {
            this.context = context;
        }
    }
}
