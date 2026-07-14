using BusinessLogicLayer.Entities;
using DataAccessLayer.DTOs;
using MongoDB.Driver;

namespace BusinessLogicLayer.ServiceContracts;

public interface IOrderService 
{
    public Task<Order?> CreateOrder(OrderDto order);
    Task<Order?> GetOrderById(Guid id);
    Task<IEnumerable<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
    Task<bool> UpdateOrder(Order order);
    Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter);
}           