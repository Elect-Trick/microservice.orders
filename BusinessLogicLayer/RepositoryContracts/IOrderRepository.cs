using BusinessLogicLayer.Entities;
using DataAccessLayer.DTOs;
using MongoDB.Driver;

namespace BusinessLogicLayer.RepositoryContracts;

public interface IOrderRepository
{
    public Task<Order?> CreateOrder(Order order);
    public Task<Order?> GetOrderById(Guid id);
    Task<IEnumerable<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
    Task<bool> UpdateOrder(Order order);
    Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter);
}