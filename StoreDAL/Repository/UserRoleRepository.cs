namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UserRoleRepository : AbstractRepository, IUserRoleRepository
{
    private readonly DbSet<UserRole> dbSet;

    public UserRoleRepository(StoreDbContext context)
        : base(context)
    {
        ArgumentNullException.ThrowIfNull(context);
        this.dbSet = context.Set<UserRole>();
    }

    public void Add(UserRole entity)
    {
        this.dbSet.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(UserRole entity)
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

    public IEnumerable<UserRole> GetAll()
    {
        return this.dbSet.ToList();
    }

    public IEnumerable<UserRole> GetAll(int pageNumber, int rowCount)
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
            .OrderBy(e => e.Id)
            .Skip((pageNumber - 1) * rowCount)
            .Take(rowCount)
            .ToList();
    }

    public UserRole GetById(int id)
    {
        return this.dbSet.Find(id);
    }

    public void Update(UserRole entity)
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