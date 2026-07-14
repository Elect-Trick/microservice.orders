using BusinessLogicLayer.Entities;
using BusinessLogicLayer.RepositoryContracts;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<Order?> GetOrderById(Guid id)
    {
        Order? order = await _orders.Find(o => o.Id.Equals(id)).FirstOrDefaultAsync();
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        List<Order> orders = await _orders.AsQueryable().ToListAsync();
        return orders;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        var result =  await _orders.DeleteOneAsync(o => o.Id.Equals(id));
        return result.DeletedCount > 0;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        foreach (var item in order.OrderItems)
        {
            item.TotalPrice = item.UnitPrice * item.Quantity;
        }
        
        order.TotalBill = order.OrderItems.Sum(o => o.TotalPrice);
        order.OrderDate = DateTime.UtcNow;
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.Id, order.Id);
        var result = await _orders.ReplaceOneAsync(filter, order);
        
        return result.MatchedCount > 0;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return await (_orders.FindAsync(filter).Result).ToListAsync();
    }
}