using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository
{
    public class ProductRepository : AbstractRepository, IProductRepository
    {
        private readonly DbSet<Product> dbSet;

        public ProductRepository(StoreDbContext? context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(context);
            this.dbSet = context.Set<Product>();
        }

        public void Add(Product? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(Product? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = this.dbSet.Find(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return this.dbSet.ToList();
        }

        public IEnumerable<Product> GetAll(int pageNumber, int rowCount)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            if (rowCount < 1)
            {
                rowCount = 10;
            }

            return this.dbSet
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();
        }

        public Product GetById(int id)
        {
            return this.dbSet
                .Include(p => p.Manufacturer)
                .Include(p => p.Title)
                .FirstOrDefault(p => p.Id == id);
        }

        public void Update(Product? entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var existing = this.dbSet.Find(entity.Id);
            if (existing != null)
            {
                this.context.Entry(existing).CurrentValues.SetValues(entity);
                this.context.SaveChanges();
            }
        }
    }
}