using BusinessLogicLayer.Entities;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersAPI.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class OrdersController: ControllerBase
{
    
   private readonly IOrderService _orderService;
   private readonly IValidator<OrderDto> _orderValidator;
   private readonly IValidator<Order> _orderUpdateValidator;
    public OrdersController(IOrderService orderService, IValidator<OrderDto> orderValidator, IValidator<Order> orderUpdateValidator)
    {
        _orderService = orderService;
        _orderValidator = orderValidator;
        _orderUpdateValidator = orderUpdateValidator;
    }

    [HttpPost]
    public async Task<ActionResult<Order?>> CreateOrder(OrderDto order)
    { 
        var validationResults = await _orderValidator.ValidateAsync(order);
       if (!validationResults.IsValid)
       {
           return BadRequest(new
           {
               Title = "Bad Request",
               Code = 500,
               Errors = validationResults.Errors.Select(error => error.ErrorMessage).ToList()
           });
       }
        Order? newOrder  = await _orderService.CreateOrder(order);
        if (newOrder == null)
        {
            return StatusCode(500, "Failed to create order");
        }
        return Ok(newOrder);
    }

    [HttpGet("search/{id}")]
    public async Task<ActionResult<Order?>> GetOrder(Guid id)
    {
        Order? order =  await _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound($"No order found with id {id}");
        }

        return Ok(order);
    }
    
    [HttpGet("productId/{id}")]
    public async Task<List<Order>> GetOrdersByProductId(Guid id)
    {
        //Probably need to search by UserId as well.
        //Perhaps also check if the product is valid(but hold on, wont this happen in the product microservice?) and in stock?
        IEnumerable<Order> orders = await _orderService.GetOrders();
        return orders.Where(o => o.OrderItems.Any(i => i.ProductId.Equals(id))).ToList();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        IEnumerable<Order> orders = await _orderService.GetOrders();
        return Ok(orders);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteOrder(Guid id)
    {
       Order? order = await _orderService.GetOrderById(id);
        if (order == null)
        {
            return NotFound($"No order found with id {id}");
        }
        
       bool result = await _orderService.DeleteOrder(id);
       if (!result)
       {
           return BadRequest($"Failed to delete order with id {id}");
       }
       
        return Ok(result);
        
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Order>> UpdateOrder(Guid id, Order order)
    {
        var validationResults = await _orderUpdateValidator.ValidateAsync(order);
        if (!validationResults.IsValid)
        {
            return BadRequest(new
            {
                Title = "Bad Request",
                Code = 500,
                Errors = validationResults.Errors.Select(error => error.ErrorMessage).ToList()
            });
        }

        if (id != order.Id)
        {
            return BadRequest($"Order with id {id} does not exist");
        }
        Order? existingOrder = await _orderService.GetOrderById(id);
        if (existingOrder == null)
        {
            return NotFound($"No order found with id {id}");
        }
        
            try
            {
                bool updated = await _orderService.UpdateOrder(order);
                if (!updated)
                {
                    return StatusCode(500, "Failed to update order");
                }
                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred while updating the order: {e.Message}");            }
      
        
    }

    [HttpGet("userId/{userId}")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUserId(Guid userId)
    {
       FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.UserId, userId);
       IEnumerable<Order> orders = await _orderService.GetOrdersByCondition(filter);
        return Ok(orders);
    }
    
    
    
}