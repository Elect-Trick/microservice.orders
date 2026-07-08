using BusinessLogicLayer.Entities;

namespace DataAccessLayer.DTOs;

public record OrderDto
{
    
  
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    
    public DateTime OrderDate { get; init; }
    
    public decimal TotalBill {  get; init; }

    public List<OrderItem> OrderItems { get; init; } = new List<OrderItem>();
    
}