using BusinessLogicLayer.Entities;
using BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class OrderController: ControllerBase
{

    
   private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("create")]
    public Task<Order?> CreateOrder(Order order)
    {
        return _orderService.CreateOrder(order);
    }
    
    
    
}