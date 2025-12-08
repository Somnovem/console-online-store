using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

namespace StoreDAL.Repository;

public class UserRepository : AbstractRepository, IUserRepository
{
    private readonly DbSet<User> dbSet;

    public UserRepository(StoreDbContext context)
        : base(context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.dbSet = context.Set<User>();
    }

    public void Add(User? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.dbSet.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(User? entity)
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
            this.Delete(entity);
        }
    }

    public IEnumerable<User> GetAll()
    {
        return this.dbSet.ToList();
    }

    public IEnumerable<User> GetAll(int pageNumber, int rowCount)
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
            .OrderBy(u => u.Id)
            .Skip((pageNumber - 1) * rowCount)
            .Take(rowCount)
            .ToList();
    }

    public User GetById(int id)
    {
        return this.dbSet.Find(id);
    }

    public void Update(User? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var existing = this.dbSet.Find(entity.Id);
        if (existing != null)
        {
            this.context.Entry(existing).CurrentValues.SetValues(entity);
            this.context.SaveChanges();
        }
    }

    public User? GetUserByLogin(string? login)
    {
        return this.dbSet.Include(u => u.Role).FirstOrDefault(u => u.Login == login);
    }
}