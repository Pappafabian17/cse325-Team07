using PizzApp.Models;

namespace PizzApp.Services;

public class CustomerOrderMockService
{
    private readonly List<CustomerOrder> _orders = new();
    private int _nextOrderNumber = 98225;

    public event Action? OnOrdersChanged;

    public CustomerOrderMockService()
    {
        InitializeMockOrders();
    }

    private void InitializeMockOrders()
    {
        _orders.Clear();

        // 1. Active Order #1: Delivery in Preparation (Eligible for cancellation)
        _orders.Add(new CustomerOrder
        {
            Id = "PA-98214",
            CreatedAt = DateTime.Now.AddMinutes(-8),
            FulfillmentType = OrderFulfillmentType.Delivery,
            Status = OrderStatus.InPreparation,
            EstimatedTime = "20 - 25 mins",
            DeliveryAddress = "525 S Center St, Rexburg, ID 83460",
            Items = new List<OrderItemMock>
            {
                new()
                {
                    Name = "Pepperoni",
                    Size = "Large",
                    Quantity = 1,
                    UnitPrice = 18.50m,
                    ImagePath = "images/pizzas/pep.png",
                    Description = "Crispy cupping pepperoni, mozzarella, signature sauce"
                },
                new()
                {
                    Name = "Cheese",
                    Size = "Medium",
                    Quantity = 1,
                    UnitPrice = 14.50m,
                    ImagePath = "images/pizzas/cheese.png",
                    Description = "Classic whole milk mozzarella and garlic herb marinara"
                }
            }
        });

        // 2. Active Order #2: Pickup Order Ready at Counter (Not eligible for cancellation)
        _orders.Add(new CustomerOrder
        {
            Id = "PA-98208",
            CreatedAt = DateTime.Now.AddMinutes(-22),
            FulfillmentType = OrderFulfillmentType.Pickup,
            Status = OrderStatus.ReadyForPickup,
            EstimatedTime = "Ready now at Counter",
            DeliveryAddress = "Store Pickup Counter: 100 Main St, Rexburg, ID",
            Items = new List<OrderItemMock>
            {
                new()
                {
                    Name = "Hawaiian",
                    Size = "Medium",
                    Quantity = 1,
                    UnitPrice = 17.00m,
                    ImagePath = "images/pizzas/hawaiian.png",
                    Description = "Smoked ham, sweet pineapple, and extra mozzarella"
                }
            }
        });

        // 3. Past Delivered Order #1
        _orders.Add(new CustomerOrder
        {
            Id = "PA-97103",
            CreatedAt = DateTime.Now.AddDays(-8),
            FulfillmentType = OrderFulfillmentType.Delivery,
            Status = OrderStatus.Delivered,
            DeliveryAddress = "525 S Center St, Rexburg, ID 83460",
            Items = new List<OrderItemMock>
            {
                new()
                {
                    Name = "Hawaiian",
                    Size = "Medium",
                    Quantity = 1,
                    UnitPrice = 17.00m,
                    ImagePath = "images/pizzas/hawaiian.png",
                    Description = "Smoked ham and pineapple with marinara"
                },
                new()
                {
                    Name = "Sausage",
                    Size = "Medium",
                    Quantity = 1,
                    UnitPrice = 16.00m,
                    ImagePath = "images/pizzas/sausage.png",
                    Description = "Italian sausage with roasted garlic and herbs"
                }
            }
        });

        // 4. Past Delivered Order #2
        _orders.Add(new CustomerOrder
        {
            Id = "PA-96024",
            CreatedAt = DateTime.Now.AddDays(-14),
            FulfillmentType = OrderFulfillmentType.Delivery,
            Status = OrderStatus.Delivered,
            DeliveryAddress = "525 S Center St, Rexburg, ID 83460",
            Items = new List<OrderItemMock>
            {
                new()
                {
                    Name = "Supreme",
                    Size = "Large",
                    Quantity = 1,
                    UnitPrice = 22.00m,
                    ImagePath = "images/pizzas/supreme.png",
                    Description = "Pepperoni, Italian sausage, mushrooms, peppers, onions"
                }
            }
        });

        // 5. Past Completed Order #3 (Pickup)
        _orders.Add(new CustomerOrder
        {
            Id = "PA-95801",
            CreatedAt = DateTime.Now.AddDays(-20),
            FulfillmentType = OrderFulfillmentType.Pickup,
            Status = OrderStatus.Completed,
            DeliveryAddress = "Store Pickup Counter",
            Items = new List<OrderItemMock>
            {
                new()
                {
                    Name = "Meat Lovers",
                    Size = "X-Large",
                    Quantity = 1,
                    UnitPrice = 25.00m,
                    ImagePath = "images/pizzas/meat.png",
                    Description = "Pepperoni, sausage, bacon, and ham"
                }
            }
        });
    }

    public List<CustomerOrder> GetAllOrders() => _orders.OrderByDescending(o => o.CreatedAt).ToList();

    public List<CustomerOrder> GetActiveOrders() => _orders.Where(o => o.IsActive).OrderByDescending(o => o.CreatedAt).ToList();

    public List<CustomerOrder> GetPastOrders() => _orders.Where(o => !o.IsActive).OrderByDescending(o => o.CreatedAt).ToList();

    public bool CancelOrder(string orderId, string reason)
    {
        var order = _orders.FirstOrDefault(o => o.Id == orderId);
        if (order == null || !order.CanCancel)
        {
            return false;
        }

        order.Status = OrderStatus.Cancelled;
        order.CancellationReason = string.IsNullOrWhiteSpace(reason) ? "Cancelled by customer" : reason;
        order.CancelledAt = DateTime.Now;

        OnOrdersChanged?.Invoke();
        return true;
    }

    public CustomerOrder? ReplicateOrder(string orderId)
    {
        var source = _orders.FirstOrDefault(o => o.Id == orderId);
        if (source == null)
        {
            return null;
        }

        // Replicate the order using current mock menu pricing and availability
        var newOrder = new CustomerOrder
        {
            Id = $"PA-{_nextOrderNumber++}",
            CreatedAt = DateTime.Now,
            FulfillmentType = source.FulfillmentType,
            Status = OrderStatus.Received,
            EstimatedTime = source.FulfillmentType == OrderFulfillmentType.Delivery ? "25 - 35 mins" : "15 - 20 mins",
            DeliveryAddress = source.DeliveryAddress,
            Items = source.Items.Select(item => new OrderItemMock
            {
                Name = item.Name,
                Size = item.Size,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice, // current pricing
                ImagePath = item.ImagePath,
                Description = item.Description
            }).ToList()
        };

        _orders.Insert(0, newOrder);
        OnOrdersChanged?.Invoke();
        return newOrder;
    }
}
