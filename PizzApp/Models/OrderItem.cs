namespace PizzApp.Models;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int? SpecialtyPizzaId { get; set; }

    public bool IsCustom { get; set; }

    public string PizzaName { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Crust { get; set; } = string.Empty;

    public string Sauce { get; set; } = string.Empty;

    public string Cheese { get; set; } = string.Empty;

    public string Toppings { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public Order Order { get; set; } = null!;
}