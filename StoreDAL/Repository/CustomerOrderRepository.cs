namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class CustomerOrderRepository : AbstractRepository, ICustomerOrderRepository
{
    public CustomerOrderRepository(StoreDbContext context)
        : base(context)
    {
    }

    public void Add(CustomerOrder entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.CustomerOrders.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(CustomerOrder entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.CustomerOrders.Remove(entity);
        this.context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var orderToDelete = this.context.CustomerOrders.Find(id);
        if (orderToDelete != null)
        {
            this.context.CustomerOrders.Remove(orderToDelete);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<CustomerOrder> GetAll()
    {
        return this.context.CustomerOrders
                   .Include(co => co.Customer)
                   .Include(co => co.OrderState)
                   .Include(co => co.CustomerOrderDetails)
                   .ToList();
    }

    public IEnumerable<CustomerOrder> GetAll(int pageNumber, int rowCount)
    {
        return this.context.CustomerOrders
                   .Include(co => co.Customer)
                   .Include(co => co.OrderState)
                   .Include(co => co.CustomerOrderDetails)
                   .Skip((pageNumber - 1) * rowCount)
                   .Take(rowCount)
                   .ToList();
    }

    public CustomerOrder GetById(int id)
    {
        return this.context.CustomerOrders
                   .Include(co => co.Customer)
                   .Include(co => co.OrderState)
                   .Include(co => co.CustomerOrderDetails)
                   .FirstOrDefault(co => co.Id == id);
    }

    public void Update(CustomerOrder entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.CustomerOrders.Update(entity);
        this.context.SaveChanges();
    }
}
