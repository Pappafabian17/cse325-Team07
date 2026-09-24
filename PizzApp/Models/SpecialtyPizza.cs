namespace PizzApp.Models;

public class SpecialtyPizza
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public decimal SmallPrice { get; set; }

    public decimal MediumPrice { get; set; }

    public decimal LargePrice { get; set; }

    public decimal XLargePrice { get; set; }

    public bool IsAvailable { get; set; } = true;
}