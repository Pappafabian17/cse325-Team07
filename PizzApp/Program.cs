using Microsoft.EntityFrameworkCore;
using PizzApp.Components;
using PizzApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Entity Framework Core for PostgreSQL / Neon.
builder.Services.AddDbContextFactory<PizzAppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbFactory =
        scope.ServiceProvider.GetRequiredService<IDbContextFactory<PizzAppDbContext>>();

    await using var db = await dbFactory.CreateDbContextAsync();

    await DbInitializer.SeedAsync(db);
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
