namespace PizzApp.Models;

public enum OrderFulfillmentType
{
    Delivery,
    Pickup
}

public enum OrderStatus
{
    Received,
    InPreparation,
    Baking,
    ReadyForPickup,
    OutForDelivery,
    Delivered,
    Completed,
    Cancelled
}

public class OrderItemMock
{
    public string Name { get; set; } = string.Empty;
    public string Size { get; set; } = "Large";
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class CustomerOrder
{
    public string Id { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public OrderFulfillmentType FulfillmentType { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemMock> Items { get; set; } = new();
    public string DeliveryAddress { get; set; } = "525 S Center St, Rexburg, ID 83460";
    public string EstimatedTime { get; set; } = "25 - 35 mins";
    public string? CancellationReason { get; set; }
    public DateTime? CancelledAt { get; set; }

    public decimal Subtotal => Items.Sum(i => i.UnitPrice * i.Quantity);
    public decimal DeliveryFee => FulfillmentType == OrderFulfillmentType.Delivery ? 3.50m : 0.00m;
    public decimal Tax => Math.Round((Subtotal + DeliveryFee) * 0.06m, 2);
    public decimal Total => Subtotal + DeliveryFee + Tax;

    public bool IsActive => Status != OrderStatus.Delivered && Status != OrderStatus.Completed && Status != OrderStatus.Cancelled;
    public bool CanCancel => Status == OrderStatus.Received || Status == OrderStatus.InPreparation;
}
