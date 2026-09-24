namespace PizzApp.Models;

public class Topping
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;
}