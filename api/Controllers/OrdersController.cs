using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _db;

    public OrdersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        
        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        
        if(customer == null)
        {
            return NotFound($"Customer with ID {request.CustomerId} not found.");
        }

        var orderItems = new List<OrderItem>();
        decimal totalAmount = 0;

        foreach (var itemRequest in request.OrderItems)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == itemRequest.ProductId);
            if (product == null)
            {
                return NotFound($"Product with ID {itemRequest.ProductId} not found.");
            }

            if (!product.IsAvailable)
            {
                return BadRequest($"Product {itemRequest.ProductId} is not available.");
            }

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemRequest.Quantity,
                UnitPrice = product.Price
            };

            orderItems.Add(orderItem);
            totalAmount += orderItem.UnitPrice * orderItem.Quantity;
        }

        
        var order = new Order
        {
            CustomerId = customer.Id,
            Customer = customer,
            OrderDate = DateTime.UtcNow,
            StreetAddress = request.StreetAddress,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country,
            OrderItems = orderItems,
            TotalAmount = totalAmount,
            Status = OrderStatus.Pending
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return Ok(OrderResponse.FromOrder(order));
    }
}