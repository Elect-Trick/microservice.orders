namespace DataAccessLayer.DTOs;

public record OrderItemDto
{
    public int Id { get; init; }
    
    public int ProductId { get; init; }
    
    public decimal UnitPrice { get; init; }
    
    public int Quantity { get; init; }
    
    public decimal TotalPrice { get; init; } 
}