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
                    Description = "Classic favorite with our rich tomato marinara and golden bubbly mozzarella.",
                    ImagePath = "images/pizzas/cheese.png",
                    SmallPrice = 12.00m,
                    MediumPrice = 14.50m,
                    LargePrice = 17.00m,
                    XLargePrice = 19.50m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Pepperoni",
                    Description = "Loaded edge-to-edge with savory, crispy-cupping pepperoni slices.",
                    ImagePath = "images/pizzas/pep.png",
                    SmallPrice = 13.50m,
                    MediumPrice = 16.00m,
                    LargePrice = 18.50m,
                    XLargePrice = 21.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Sausage",
                    Description = "Savory Italian pork sausage blended with roasted garlic and Italian seasonings.",
                    ImagePath = "images/pizzas/sausage.png",
                    SmallPrice = 13.50m,
                    MediumPrice = 16.00m,
                    LargePrice = 18.50m,
                    XLargePrice = 21.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Hawaiian",
                    Description = "Sweet pineapple tidbits and savory smoked Canadian ham over melted cheese.",
                    ImagePath = "images/pizzas/hawaiian.png",
                    SmallPrice = 14.50m,
                    MediumPrice = 17.00m,
                    LargePrice = 19.50m,
                    XLargePrice = 22.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Veggie",
                    Description = "Crisp bell peppers, red onions, earthy mushrooms, and ripe black olives.",
                    ImagePath = "images/pizzas/veggie.png",
                    SmallPrice = 14.00m,
                    MediumPrice = 16.50m,
                    LargePrice = 19.00m,
                    XLargePrice = 21.50m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Meat Lovers",
                    Description = "A hearty carnivore blend of pepperoni, Italian sausage, bacon, and ham.",
                    ImagePath = "images/pizzas/meat.png",
                    SmallPrice = 16.00m,
                    MediumPrice = 19.00m,
                    LargePrice = 22.00m,
                    XLargePrice = 25.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "Supreme",
                    Description = "The ultimate combination of meats, garden fresh veggies, and melted cheeses.",
                    ImagePath = "images/pizzas/supreme.png",
                    SmallPrice = 16.00m,
                    MediumPrice = 19.00m,
                    LargePrice = 22.00m,
                    XLargePrice = 25.00m,
                    IsAvailable = true
                },
                new SpecialtyPizza
                {
                    Name = "House Special",
                    Description = "Chef's specialty with garlic butter crust, tender grilled chicken, bacon, and herbs.",
                    ImagePath = "images/pizzas/house.png",
                    SmallPrice = 16.50m,
                    MediumPrice = 19.50m,
                    LargePrice = 22.50m,
                    XLargePrice = 25.50m,
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