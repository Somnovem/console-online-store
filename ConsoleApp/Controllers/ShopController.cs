namespace ConsoleApp.Controllers;

using System;
using StoreBLL.Models;
using StoreBLL.Services;
using ConsoleApp.Helpers;
using System.Linq;

public class ShopController
{
    private readonly CustomerOrderService customerOrderService;
    private readonly OrderDetailService orderDetailService;
    private readonly OrderStateService orderStateService;

    public ShopController(
        CustomerOrderService customerOrderService,
        OrderDetailService orderDetailService,
        OrderStateService orderStateService)
    {
        this.customerOrderService = customerOrderService ?? throw new ArgumentNullException(nameof(customerOrderService));
        this.orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
        this.orderStateService = orderStateService ?? throw new ArgumentNullException(nameof(orderStateService));
    }

    public void AddOrder()
    {
        Console.WriteLine("\n--- Create New Order ---");
        try
        {
            var order = InputHelper.ReadCustomerOrderModel();
            this.customerOrderService.Add(order);
            Console.WriteLine("Order created successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error creating order: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error creating order: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error creating order: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateOrder()
    {
        Console.WriteLine("\n--- Update Order ---");
        try
        {
            int id = InputHelper.ReadInt("Order ID to update");
            var existingOrder = this.customerOrderService.GetById(id) as CustomerOrderModel;
            if (existingOrder == null)
            {
                Console.WriteLine($"Order with ID {id} not found.");
                return;
            }

            var updatedOrder = InputHelper.ReadCustomerOrderModel();
            updatedOrder.Id = id;
            this.customerOrderService.Update(updatedOrder);
            Console.WriteLine($"Order with ID {id} updated successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error updating order: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error updating order: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error updating order: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteOrder()
    {
        Console.WriteLine("\n--- Cancel Order ---");
        try
        {
            int id = InputHelper.ReadInt("Order ID to cancel/delete");
            this.customerOrderService.Delete(id);
            Console.WriteLine($"Order with ID {id} cancelled/deleted successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error cancelling/deleting order: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error cancelling/deleting order: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error cancelling/deleting order: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowOrder()
    {
        Console.WriteLine("\n--- Show Order Details ---");
        try
        {
            int id = InputHelper.ReadInt("Order ID to view");
            var order = this.customerOrderService.GetById(id);
            if (order != null)
            {
                Console.WriteLine(order.ToString());
            }
            else
            {
                Console.WriteLine($"Order with ID {id} not found.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error showing order: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error showing order: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error showing order: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllOrders()
    {
        Console.WriteLine("\n--- All Orders ---");
        try
        {
            var orders = this.customerOrderService.GetAll().ToList();
            if (orders.Count != 0)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine(order.ToString());
                }
            }
            else
            {
                Console.WriteLine("No orders found.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error showing all orders: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error showing all orders: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error showing all orders: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ProcessOrder()
    {
        Console.WriteLine("\n--- Change Order Status ---");
        try
        {
            int orderId = InputHelper.ReadInt("Order ID");
            var order = this.customerOrderService.GetById(orderId) as CustomerOrderModel;
            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found.");
                return;
            }

            Console.WriteLine("\nAvailable Order States:");
            this.ShowAllOrderStates();

            int newStateId = InputHelper.ReadInt("New Order State ID");
            var newState = this.orderStateService.GetById(newStateId) as OrderStateModel;
            if (newState == null)
            {
                Console.WriteLine($"Order State with ID {newStateId} not found.");
                return;
            }

            order.OrderStateId = newStateId;
            this.customerOrderService.Update(order);
            Console.WriteLine($"Order {orderId} status changed to {newState.StateName} successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"An error occurred while changing order status: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"An error occurred while changing order status: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error while changing order status: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void AddOrderDetails()
    {
        Console.WriteLine("\n--- Add Order Detail ---");
        try
        {
            var orderDetail = InputHelper.ReadOrderDetailModel();
            this.orderDetailService.Add(orderDetail);
            Console.WriteLine("Order Detail added successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error adding order detail: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error adding order detail: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error adding order detail: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void UpdateOrderDetails()
    {
        Console.WriteLine("\n--- Update Order Detail ---");
        try
        {
            int id = InputHelper.ReadInt("Order Detail ID to update");
            var existingDetail = this.orderDetailService.GetById(id);
            if (existingDetail == null)
            {
                Console.WriteLine($"Order Detail with ID {id} not found.");
                return;
            }

            var updatedDetail = InputHelper.ReadOrderDetailModel();
            updatedDetail.Id = id;
            this.orderDetailService.Update(updatedDetail);
            Console.WriteLine($"Order Detail with ID {id} updated successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error updating order detail: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error updating order detail: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error updating order detail: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void DeleteOrderDetails()
    {
        Console.WriteLine("\n--- Delete Order Detail ---");
        try
        {
            int id = InputHelper.ReadInt("Order Detail ID to delete");
            this.orderDetailService.Delete(id);
            Console.WriteLine($"Order Detail with ID {id} deleted successfully!");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error deleting order detail: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error deleting order detail: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error deleting order detail: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllOrderDetails()
    {
        Console.WriteLine("\n--- All Order Details ---");
        try
        {
            var orderDetails = this.orderDetailService.GetAll().ToList();
            if (orderDetails.Count != 0)
            {
                foreach (var detail in orderDetails)
                {
                    Console.WriteLine(detail.ToString());
                }
            }
            else
            {
                Console.WriteLine("No order details found.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error showing all order details: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error showing all order details: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error showing all order details: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }

    public void ShowAllOrderStates()
    {
        Console.WriteLine("\n--- All Order States ---");
        try
        {
            var orderStates = this.orderStateService.GetAll().ToList();
            if (orderStates.Count != 0)
            {
                foreach (var state in orderStates)
                {
                    Console.WriteLine(state.ToString());
                }
            }
            else
            {
                Console.WriteLine("No order states found.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error showing all order states: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error showing all order states: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error showing all order states: {ex.Message}");
            throw;
        }

        InputHelper.PressAnyKeyToContinue();
    }
}
