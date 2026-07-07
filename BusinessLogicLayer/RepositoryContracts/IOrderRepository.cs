using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.RepositoryContracts;

public interface IOrderRepository
{
    public Task<Order?> CreateOrder(Order order);
}