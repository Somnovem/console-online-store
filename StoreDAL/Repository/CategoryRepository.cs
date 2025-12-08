namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class CategoryRepository : AbstractRepository, ICategoryRepository
{
    public CategoryRepository(StoreDbContext context)
        : base(context)
    {
    }

    public void Add(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Categories.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Categories.Remove(entity);
        this.context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var categoryToDelete = this.context.Categories.Find(id);
        if (categoryToDelete != null)
        {
            this.context.Categories.Remove(categoryToDelete);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<Category> GetAll()
    {
        return this.context.Categories.ToList();
    }

    public IEnumerable<Category> GetAll(int pageNumber, int rowCount)
    {
        return this.context.Categories
                   .Skip((pageNumber - 1) * rowCount)
                   .Take(rowCount)
                   .ToList();
    }

    public Category GetById(int id)
    {
        return this.context.Categories.Find(id);
    }

    public void Update(Category entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Categories.Update(entity);
        this.context.SaveChanges();
    }
}
