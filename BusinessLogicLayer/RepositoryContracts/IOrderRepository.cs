using BusinessLogicLayer.Entities;
using DataAccessLayer.DTOs;

namespace BusinessLogicLayer.RepositoryContracts;

public interface IOrderRepository
{
    public Task<Order?> CreateOrder(Order order);
    public Task<Order?> GetOrderById(Guid id);
    Task<IEnumerable<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
}