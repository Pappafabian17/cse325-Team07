using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzApp.Models;

public class CartItem
{
    public Guid CartItemId { get; set; } = Guid.NewGuid();

    public bool IsCustom { get; set; }

    public int PizzaId { get; set; }

    public string PizzaName { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Size { get; set; } = "Medium";

    public string Crust { get; set; } = "Hand Tossed";

    public string Sauce { get; set; } = "Marinara";

    public string Cheese { get; set; } = "Mozzarella";

    public decimal BasePrice { get; set; }

    public List<Topping> Toppings { get; set; } = new();

    public int Quantity { get; set; } = 1;

    public decimal ToppingsTotal =>
        Toppings.Sum(topping => topping.Price);

    public decimal UnitPrice =>
        BasePrice + ToppingsTotal;

    public decimal TotalPrice =>
        UnitPrice * Quantity;

    public CartItem Copy()
    {
        return new CartItem
        {
            CartItemId = CartItemId,
            IsCustom = IsCustom,
            PizzaId = PizzaId,
            PizzaName = PizzaName,
            ImagePath = ImagePath,
            Size = Size,
            Crust = Crust,
            Sauce = Sauce,
            Cheese = Cheese,
            BasePrice = BasePrice,
            Quantity = Quantity,

            Toppings = Toppings
                .Select(topping => new Topping
                {
                    Id = topping.Id,
                    Name = topping.Name,
                    ImagePath = topping.ImagePath,
                    Price = topping.Price,
                    IsAvailable = topping.IsAvailable
                })
                .ToList()
        };
    }
}