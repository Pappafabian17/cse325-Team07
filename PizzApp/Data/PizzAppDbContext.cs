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
    }
}