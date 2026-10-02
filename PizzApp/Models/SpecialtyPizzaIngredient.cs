namespace PizzApp.Models;

public class SpecialtyPizzaIngredient
{
    public int Id { get; set; }

    public int SpecialtyPizzaId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public SpecialtyPizza SpecialtyPizza { get; set; } = null!;
}