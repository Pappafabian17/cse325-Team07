using Microsoft.EntityFrameworkCore;
using PizzApp.Models;

namespace PizzApp.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(PizzAppDbContext db)
    {
        await db.Database.MigrateAsync();

        if (!await db.SpecialtyPizzas.AnyAsync())
        {
            db.SpecialtyPizzas.AddRange(
                new SpecialtyPizza
                {
                    Name = "Cheese",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/cheese.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Pepperoni",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/pep.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Sausage",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/sausage.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Hawaiian",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/hawaiian.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Veggie",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/veggie.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Meat Lovers",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/meat.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Supreme",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/supreme.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "House Special",
                    Description = "Pricing and recipe details to be finalized.",
                    ImagePath = "images/pizzas/house.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 12.00m,
                    LargePrice = 12.00m,
                    XLargePrice = 12.00m,
                    IsAvailable = true
                }
            );
        }

        if (!await db.Toppings.AnyAsync())
        {
            db.Toppings.AddRange(
                new Topping
                {
                    Name = "Pepperoni",
                    ImagePath = "images/toppings/pepperoni.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Italian Sausage",
                    ImagePath = "images/toppings/sausage.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Bacon",
                    ImagePath = "images/toppings/bacon.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Ham",
                    ImagePath = "images/toppings/ham.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Chicken",
                    ImagePath = "images/toppings/chicken.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Mushrooms",
                    ImagePath = "images/toppings/mushroom.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Black Olives",
                    ImagePath = "images/toppings/olives.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Onions",
                    ImagePath = "images/toppings/onions.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Green Peppers",
                    ImagePath = "images/toppings/green-peppers.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Pineapple",
                    ImagePath = "images/toppings/pineapple.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Jalapeños",
                    ImagePath = "images/toppings/jalapenos.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Tomatoes",
                    ImagePath = "images/toppings/tomato.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Spinach",
                    ImagePath = "images/toppings/spinach.png",
                    Price = 1.50m,
                    IsAvailable = true
                },
                new Topping
                {
                    Name = "Extra Cheese",
                    ImagePath = "images/toppings/extra-cheese.png",
                    Price = 1.50m,
                    IsAvailable = true
                }
            );
        }

        await db.SaveChangesAsync();
    }
}