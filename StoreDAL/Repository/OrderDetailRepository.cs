namespace StoreDAL.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreDAL.Data;
using StoreDAL.Entities;
using StoreDAL.Interfaces;

public class OrderDetailRepository : AbstractRepository, IOrderDetailRepository
{
    public OrderDetailRepository(StoreDbContext context)
        : base(context)
    {
    }

    public void Add(OrderDetail entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.OrderDetails.Add(entity);
        this.context.SaveChanges();
    }

    public void Delete(OrderDetail entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.OrderDetails.Remove(entity);
        this.context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var orderDetailToDelete = this.context.OrderDetails.Find(id);
        if (orderDetailToDelete != null)
        {
            this.context.OrderDetails.Remove(orderDetailToDelete);
            this.context.SaveChanges();
        }
    }

    public IEnumerable<OrderDetail> GetAll()
    {
        return this.context.OrderDetails
                   .Include(od => od.CustomerOrder)
                   .Include(od => od.Product)
                   .ToList();
    }

    public IEnumerable<OrderDetail> GetAll(int pageNumber, int rowCount)
    {
        return this.context.OrderDetails
                   .Include(od => od.CustomerOrder)
                   .Include(od => od.Product)
                   .Skip((pageNumber - 1) * rowCount)
                   .Take(rowCount)
                   .ToList();
    }

    public OrderDetail GetById(int id)
    {
        return this.context.OrderDetails
                   .Include(od => od.CustomerOrder)
                   .Include(od => od.Product)
                   .FirstOrDefault(od => od.Id == id);
    }

    public void Update(OrderDetail entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this.context.OrderDetails.Update(entity);
        this.context.SaveChanges();
    }
}
