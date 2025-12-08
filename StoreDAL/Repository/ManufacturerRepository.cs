namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class ManufacturerRepository : AbstractRepository, IManufacturerRepository
{
    public ManufacturerRepository(StoreDbContext context)
        : base(context)
    {
    }

    public void Add(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Manufacturers.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Manufacturers.Remove(entity);
        this.context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var manufacturerToDelete = this.context.Manufacturers.Find(id);
        if (manufacturerToDelete != null)
        {
            this.context.Manufacturers.Remove(manufacturerToDelete);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<Manufacturer> GetAll()
    {
        return this.context.Manufacturers.ToList();
    }

    public IEnumerable<Manufacturer> GetAll(int pageNumber, int rowCount)
    {
        return this.context.Manufacturers
                   .Skip((pageNumber - 1) * rowCount)
                   .Take(rowCount)
                   .ToList();
    }

    public Manufacturer GetById(int id)
    {
        return this.context.Manufacturers.Find(id);
    }

    public void Update(Manufacturer entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.Manufacturers.Update(entity);
        this.context.SaveChanges();
    }
}
