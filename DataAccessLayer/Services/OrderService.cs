using BusinessLogicLayer.Entities;
using BusinessLogicLayer.RepositoryContracts;
using BusinessLogicLayer.ServiceContracts;

namespace DataAccessLayer.Services;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<Order?> CreateOrder(Order order)
    {
       Order? newOrder =  await _orderRepository.CreateOrder(order);
       
       return newOrder;
    }
}