using BusinessLogicLayer.Entities;
using BusinessLogicLayer.RepositoryContracts;
using MongoDB.Driver;

namespace DataAccessLayer.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;
    public OrderRepository(MongoDbContext.MongoDbContext mongoDbContext)
    {
        _orders = mongoDbContext.Orders;
    }
    public async Task<Order?> CreateOrder(Order order)
    {
        Order newOrder = new Order()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(), 
            OrderDate = DateTime.UtcNow,
            TotalBill = 159.99m,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = "Laptop",
                    Quantity = 1,
                    UnitPrice = 999.99m
                }
            }
            
        };
        await _orders.InsertOneAsync(newOrder);
        return newOrder;
    }
}