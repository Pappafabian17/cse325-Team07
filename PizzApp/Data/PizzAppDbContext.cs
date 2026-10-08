using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzApp.Models;

namespace PizzApp.Data;

public class PizzAppDbContext : IdentityDbContext<ApplicationUser>
{
    public PizzAppDbContext(DbContextOptions<PizzAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SpecialtyPizza> SpecialtyPizzas => Set<SpecialtyPizza>();

    public DbSet<Topping> Toppings => Set<Topping>();

    public DbSet<SpecialtyPizzaIngredient> SpecialtyPizzaIngredients =>
        Set<SpecialtyPizzaIngredient>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<StoreSettings> StoreSettings => Set<StoreSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SpecialtyPizza>(entity =>
        {
            entity.HasIndex(pizza => pizza.Name)
                .IsUnique();

            entity.Property(pizza => pizza.SmallPrice)
                .HasPrecision(10, 2);

            entity.Property(pizza => pizza.MediumPrice)
                .HasPrecision(10, 2);

            entity.Property(pizza => pizza.LargePrice)
                .HasPrecision(10, 2);

            entity.Property(pizza => pizza.XLargePrice)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<SpecialtyPizzaIngredient>(entity =>
        {
            entity.HasOne(ingredient => ingredient.SpecialtyPizza)
                .WithMany(pizza => pizza.Ingredients)
                .HasForeignKey(ingredient => ingredient.SpecialtyPizzaId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(ingredient => new
            {
                ingredient.SpecialtyPizzaId,
                ingredient.SortOrder
            });
        });

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasIndex(topping => topping.Name)
                .IsUnique();

            entity.Property(topping => topping.Price)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            // Store enum names in the database so order status and fulfillment records remain readable.
            entity.Property(order => order.FulfillmentType)
                .HasConversion<string>();

            entity.Property(order => order.Status)
                .HasConversion<string>();

            entity.Property(order => order.Subtotal)
                .HasPrecision(10, 2);

            entity.Property(order => order.Tax)
                .HasPrecision(10, 2);

            entity.Property(order => order.DeliveryFee)
                .HasPrecision(10, 2);

            entity.Property(order => order.Total)
                .HasPrecision(10, 2);

            entity.HasOne(order => order.Customer)
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                // Retain customer and order history together rather than cascading user deletion into orders.
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(order => new
            {
                order.CustomerId,
                order.PlacedAt
            });
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(item => item.UnitPrice)
                .HasPrecision(10, 2);

            entity.Property(item => item.TotalPrice)
                .HasPrecision(10, 2);

            entity.HasOne(item => item.Order)
                .WithMany(order => order.Items)
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StoreSettings>(entity =>
        {
            entity.Property(settings => settings.DeliveryFee)
                .HasPrecision(10, 2);

            entity.Property(settings => settings.SalesTaxPercent)
                .HasPrecision(5, 2);
        });
    }
}
