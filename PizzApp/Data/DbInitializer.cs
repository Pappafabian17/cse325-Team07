using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzApp.Models;

namespace PizzApp.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(
        PizzAppDbContext db,
        RoleManager<IdentityRole>? roleManager = null,
        UserManager<ApplicationUser>? userManager = null)
    {
        // Apply pending schema changes first, then seed only missing baseline data so admin edits survive restarts.
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

        if (!await db.SpecialtyPizzaIngredients.AnyAsync())
        {
            var pizzas = await db.SpecialtyPizzas
                .ToDictionaryAsync(pizza => pizza.Name);

            var ingredients = new List<SpecialtyPizzaIngredient>();

            ingredients.AddRange(CreateIngredients(
                pizzas["Cheese"],
                "Mozzarella",
                "Marinara Sauce",
                "Oregano",
                "Parmesan"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Pepperoni"],
                "Pepperoni",
                "Mozzarella",
                "Marinara Sauce"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Sausage"],
                "Italian Sausage",
                "Mozzarella",
                "Marinara Sauce",
                "Garlic"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Hawaiian"],
                "Smoked Ham",
                "Pineapple",
                "Mozzarella",
                "Marinara Sauce"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Veggie"],
                "Mushrooms",
                "Bell Peppers",
                "Red Onions",
                "Black Olives",
                "Mozzarella"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Meat Lovers"],
                "Pepperoni",
                "Italian Sausage",
                "Smoked Bacon",
                "Ham",
                "Mozzarella"));

            ingredients.AddRange(CreateIngredients(
                pizzas["Supreme"],
                "Pepperoni",
                "Italian Sausage",
                "Mushrooms",
                "Bell Peppers",
                "Onions"));

            ingredients.AddRange(CreateIngredients(
                pizzas["House Special"],
                "Grilled Chicken",
                "Smoked Bacon",
                "Garlic Butter",
                "Red Onions",
                "Mozzarella"));

            db.SpecialtyPizzaIngredients.AddRange(ingredients);

            await db.SaveChangesAsync();
        }

        if (!await db.StoreSettings.AnyAsync())
        {
            db.StoreSettings.Add(
                new StoreSettings
                {
                    OperatingHours = "11:00 AM – 10:00 PM Daily",
                    DeliveryRadiusMiles = 8,
                    DeliveryFee = 3.50m,
                    SalesTaxPercent = 6.00m
                });

            await db.SaveChangesAsync();
        }

        if (roleManager != null && userManager != null)
        {
            await SeedRolesAndUsersAsync(roleManager, userManager);
        }
    }

    private static IEnumerable<SpecialtyPizzaIngredient> CreateIngredients(
        SpecialtyPizza pizza,
        params string[] ingredientNames)
    {
        return ingredientNames.Select((name, index) =>
            new SpecialtyPizzaIngredient
            {
                SpecialtyPizzaId = pizza.Id,
                Name = name,
                SortOrder = index + 1
            });
    }

    private static async Task SeedRolesAndUsersAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        // 1. Seed Customer, Staff, and Administrator Roles
        string[] roles = ["Administrator", "Staff", "Customer"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Seed Default Test Accounts for easy grading and testing
        var defaultUsers = new (string Email, string Name, string Role, string Password, string? Address)[]
        {
            ("admin@pizzapp.com", "Jordan Administrator", "Administrator", "Admin123!", "100 Admin Way, Rexburg, ID 83440"),
            ("staff@pizzapp.com", "Sam Kitchen Staff", "Staff", "Staff123!", "200 Kitchen Blvd, Rexburg, ID 83440"),
            ("customer@pizzapp.com", "Alex Customer", "Customer", "Customer123!", "300 Customer Ln, Rexburg, ID 83440")
        };

        foreach (var (email, name, role, password, address) in defaultUsers)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            // Do not reset an existing account's password, profile, or role on later startups.
            if (existingUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = name,
                    DeliveryAddress = address,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(user, password);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
