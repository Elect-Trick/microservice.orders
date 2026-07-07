using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.RepositoryContracts;

public interface IOrderRepository
{
    public Task<Order?> CreateOrder(Order order);
    public Task<Order?> GetOrderById(Guid id);
    Task<List<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
}