using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessLogicLayer.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public DateTime OrderDate { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalBill {  get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}