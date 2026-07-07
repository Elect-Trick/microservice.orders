using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.ServiceContracts;

public interface IOrderService 
{
    public Task<Order?> CreateOrder(Order order);
}