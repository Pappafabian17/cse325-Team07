using Microsoft.EntityFrameworkCore;
using PizzApp.Components;
using PizzApp.Data;
using PizzApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register purely frontend UI state and mock order management
builder.Services.AddScoped<FrontendUserState>();
builder.Services.AddScoped<CustomerOrderMockService>();

// Configure Entity Framework Core for PostgreSQL / Neon.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<PizzAppDbContext>(options =>
{
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseNpgsql("Host=localhost;Database=pizzapp_dev;Username=postgres;Password=postgres");
    }
});

var app = builder.Build();

if (!string.IsNullOrWhiteSpace(connectionString))
{
    try
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PizzAppDbContext>>();
        await using var db = await dbFactory.CreateDbContextAsync();
        await DbInitializer.SeedAsync(db);
    }
    catch (Exception ex)
    {
        app.Logger.LogWarning(ex, "Could not initialize database. Continuing in offline mode.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute(
    "/not-found",
    createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
