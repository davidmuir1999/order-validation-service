using Microsoft.EntityFrameworkCore;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    
    [Precision(18, 2)]
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}