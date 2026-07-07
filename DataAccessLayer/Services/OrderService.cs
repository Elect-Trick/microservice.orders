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

    public async Task<Order?> GetOrderById(Guid id)
    {
        Order? order = await _orderRepository.GetOrderById(id);
        return order;
    }

    public async Task<List<Order>> GetOrders()
    {
        List<Order> orders = await _orderRepository.GetOrders();
        return orders;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        return await _orderRepository.DeleteOrder(id);
    }
}