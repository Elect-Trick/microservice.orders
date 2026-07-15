namespace DataAccessLayer.DTOs;

public record UserDTO
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public char? Gender { get; set; }
    
}