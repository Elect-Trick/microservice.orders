using BusinessLogicLayer.Entities;
using DataAccessLayer.DTOs;
namespace BusinessLogicLayer.ServiceContracts;

public interface IOrderService 
{
    public Task<Order?> CreateOrder(OrderDto order);
    Task<Order?> GetOrderById(Guid id);
    Task<IEnumerable<Order>> GetOrders();
    Task<bool> DeleteOrder(Guid id);
}           