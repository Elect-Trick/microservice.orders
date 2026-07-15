using AutoMapper;
using BusinessLogicLayer.Entities;
using BusinessLogicLayer.HttpClient;
using BusinessLogicLayer.RepositoryContracts;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.DTOs;
using MongoDB.Driver;

namespace DataAccessLayer.Services;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly UserMicroServiceClient _userMicroServiceClient;
    public OrderService(IOrderRepository orderRepository, IMapper mapper, UserMicroServiceClient userMicroServiceClient)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userMicroServiceClient = userMicroServiceClient;
    }
    public async Task<Order?> CreateOrder(OrderDto order)
    {
        
       UserDTO? user = await  _userMicroServiceClient.GetUserByUserID(order.UserId);
       if (user == null)
       {
           throw new ArgumentNullException("Invalid user ID");
       }
       
        Order orderEntity = _mapper.Map<Order>(order);
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
        return await _orderRepository.UpdateOrder(order);
    }

    public async Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return await _orderRepository.GetOrdersByCondition(filter);
    }
}