using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.HttpClient;
using BusinessLogicLayer.RepositoryContracts;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.DTOs;
using FluentValidation;
using MongoDB.Driver;

namespace DataAccessLayer.Services;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly UserMicroServiceClient _userMicroServiceClient;
    private readonly ProductMicroServiceClient _productMicroServiceClient;
    private readonly IValidator<OrderDto> _orderValidator;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, UserMicroServiceClient userMicroServiceClient, ProductMicroServiceClient productMicroServiceClient, IValidator<OrderDto> orderValidator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userMicroServiceClient = userMicroServiceClient;
        _productMicroServiceClient = productMicroServiceClient;
        _orderValidator = orderValidator;
    }
    public async Task<Order?> CreateOrder(OrderDto order)
    {
        
       UserDTO? user = await  _userMicroServiceClient.GetUserByUserID(order.UserId);
       if (user == null)
       {
           throw new ArgumentNullException("Invalid user ID");
       }
        
       
       foreach (var item in order.OrderItems)
       {
           ProductDTO? product = await _productMicroServiceClient.GetProductById(item.ProductId);
           if (product == null)
           {
               throw new ArgumentNullException($"Invalid Product ID {item.ProductId} ");
           }

           item.ProductName = product.ProductName;
           item.UnitPrice = (decimal)product.UnitPrice!;
           
       }
       
        Order orderEntity = _mapper.Map<Order>(order);
        
        var validationResults = await _orderValidator.ValidateAsync(order);
        if (!validationResults.IsValid)
        {
            var errors = validationResults.Errors.Select(error => error.ErrorMessage).ToList();
            throw new Exception(string.Join(", ", errors));
        }
        orderEntity.OrderDate = DateTime.UtcNow;
        Order? newOrder =  await _orderRepository.CreateOrder(orderEntity);
       
       return newOrder;
    }

    public async Task<Order?> GetOrderById(Guid id)
    {
        Order? order = await _orderRepository.GetOrderById(id);
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        
        IEnumerable<Order> orders = await _orderRepository.GetOrders();
        return orders;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        return await _orderRepository.DeleteOrder(id);
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        UserDTO? user = await  _userMicroServiceClient.GetUserByUserID(order.UserId);
        if (user == null)
        {
            throw new ArgumentNullException("Invalid user ID");
        }
        
        foreach (var item in order.OrderItems)
        {
            ProductDTO? product = await _productMicroServiceClient.GetProductById(item.ProductId);
            if (product == null)
            {
                throw new ArgumentNullException($"Invalid Product ID {item.ProductId} ");
            }
        }
        return await _orderRepository.UpdateOrder(order);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return await _orderRepository.GetOrdersByCondition(filter);
    }
}