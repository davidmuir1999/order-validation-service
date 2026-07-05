using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public record CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public record CreateOrderRequest
{
    public int CustomerId { get; set; }
    public required string StreetAddress { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }

    public List<CreateOrderItemRequest> OrderItems { get; set; } = new List<CreateOrderItemRequest>();

}