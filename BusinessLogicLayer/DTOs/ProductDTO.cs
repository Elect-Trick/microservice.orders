namespace DataAccessLayer.DTOs;

public record ProductDTO
{
    
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
    
}