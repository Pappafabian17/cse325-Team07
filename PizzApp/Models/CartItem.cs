using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzApp.Models;

public class CartItem
{
    public int PizzaId { get; set; }

    public string PizzaName { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Size { get; set; } = "Medium";

    public decimal BasePrice { get; set; }

    public List<Topping> Toppings { get; set; } = new();

    public int Quantity { get; set; } = 1;

    public decimal ToppingsTotal =>
        Toppings.Sum(topping => topping.Price);

    public decimal UnitPrice =>
        BasePrice + ToppingsTotal;

    public decimal TotalPrice =>
        UnitPrice * Quantity;
}