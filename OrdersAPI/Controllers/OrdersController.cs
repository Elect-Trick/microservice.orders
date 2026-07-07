using BusinessLogicLayer.Entities;
using BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class OrdersController: ControllerBase
{
    
   private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<Order?> CreateOrder(Order order)
    {
        return await _orderService.CreateOrder(order);
    }

    [HttpGet("search/{id}")]
    public async Task<Order?> GetOrder(Guid id)
    {
        return await _orderService.GetOrderById(id);
    }
    
    [HttpGet("productId/{id}")]
    public async Task<List<Order>> GetOrdersByProductId(Guid id)
    {
        List<Order> orders = await _orderService.GetOrders();
        return orders.Where(o => o.OrderItems.Any(i => i.ProductId.Equals(id))).ToList();
    }
    
    [HttpGet]
    public async Task<List<Order>> GetOrders()
    {
        List<Order> orders = await _orderService.GetOrders();
        return orders;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteOrder(Guid id)
    {
       Order? order = await _orderService.GetOrderById(id);
        if (order == null)
        {
            return false;
        }
        await _orderService.DeleteOrder(id);
        return true;
        
    }
    
    
    
}