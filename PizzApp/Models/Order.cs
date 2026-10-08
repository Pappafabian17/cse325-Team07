using System.ComponentModel.DataAnnotations.Schema;

namespace PizzApp.Models;

public class Order
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public FulfillmentType FulfillmentType { get; set; }

    public string? DeliveryAddress { get; set; }

    public DateTimeOffset PlacedAt { get; set; } = DateTimeOffset.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Received;

    public string? CancellationReason { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Tax { get; set; }

    public decimal DeliveryFee { get; set; }

    public decimal Total { get; set; }

    public ApplicationUser Customer { get; set; } = null!;

    public List<OrderItem> Items { get; set; } = new();

    [NotMapped]
    public string OrderNumber => $"PA-{10000 + Id:D5}";
}