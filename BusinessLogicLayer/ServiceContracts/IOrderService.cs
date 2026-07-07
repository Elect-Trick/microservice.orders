using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.ServiceContracts;

public interface IOrderService 
{
    public Task<Order?> CreateOrder(Order order);
    Task<Order?> GetOrderById(Guid id);
    Task<List<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
}           