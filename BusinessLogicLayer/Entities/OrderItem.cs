using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessLogicLayer.Entities;

public class OrderItem
{

    public OrderItem()
    {
        //We need to create a new id for each order item since this is a nested document(object).
        Id = Guid.NewGuid();
    }
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public int ProductId { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string? ProductName{ get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal UnitPrice { get; set; }
    
    [BsonRepresentation(BsonType.Int32)]
    public int Quantity { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal TotalPrice { get; set; } 
}