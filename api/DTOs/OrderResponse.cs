public record CustomerResponse
{
    public required string FirstName { get; init; }
    public required string Email { get; init; }
}

public record OrderItemResponse
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public required string ProductName { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}

public record OrderResponse
{
    public int Id { get; init; }
    public required CustomerResponse Customer { get; init; }
    public decimal TotalAmount { get; init; }
    public required string StreetAddress { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public List<OrderItemResponse> OrderItems { get; init; } = new List<OrderItemResponse>();

    public static OrderResponse FromOrder(Order order) => new()
    {
        Id = order.Id,
        Customer = new CustomerResponse
        {
            FirstName = order.Customer.FirstName,
            Email = order.Customer.Email
        },
        TotalAmount = order.TotalAmount,
        StreetAddress = order.StreetAddress,
        City = order.City,
        State = order.State,
        PostalCode = order.PostalCode,
        Country = order.Country,
        OrderDate = order.OrderDate,
        Status = order.Status,
        OrderItems = order.OrderItems.Select(oi => new OrderItemResponse
        {
            Id = oi.Id,
            ProductId = oi.ProductId,
            ProductName = oi.Product.Name,
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList()
    };
}
