namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class UserRepository : AbstractRepository, IUserRepository
{
    public UserRepository(StoreDbContext context)
        : base(context)
    {
    }

    public void Add(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Users.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Users.Remove(entity);
        this.context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var userToDelete = this.context.Users.Find(id);
        if (userToDelete != null)
        {
            this.context.Users.Remove(userToDelete);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<User> GetAll()
    {
        return this.context.Users
                   .Include(u => u.UserRole)
                   .ToList();
    }

    public IEnumerable<User> GetAll(int pageNumber, int rowCount)
    {
        return this.context.Users
                   .Include(u => u.UserRole)
                   .Skip((pageNumber - 1) * rowCount)
                   .Take(rowCount)
                   .ToList();
    }

    public User GetById(int id)
    {
        return this.context.Users
                   .Include(u => u.UserRole)
                   .FirstOrDefault(u => u.Id == id);
    }

    public void Update(User entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Users.Update(entity);
        this.context.SaveChanges();
    }
}
