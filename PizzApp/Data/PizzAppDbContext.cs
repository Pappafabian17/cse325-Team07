using Microsoft.EntityFrameworkCore;
using PizzApp.Models;

namespace PizzApp.Data;

public class PizzAppDbContext : DbContext
{
    public PizzAppDbContext(DbContextOptions<PizzAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<SpecialtyPizza> SpecialtyPizzas => Set<SpecialtyPizza>();

    public DbSet<Topping> Toppings => Set<Topping>();

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

        modelBuilder.Entity<Topping>(entity =>
        {
            entity.HasIndex(topping => topping.Name)
                .IsUnique();

            entity.Property(topping => topping.Price)
                .HasPrecision(10, 2);
        });
    }
}