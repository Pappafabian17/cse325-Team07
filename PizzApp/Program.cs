using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzApp.Components;
using PizzApp.Data;
using PizzApp.Endpoints;
using PizzApp.Models;
using PizzApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Cascading authentication state for Blazor components (AuthorizeView, AuthorizeRouteView)
builder.Services.AddCascadingAuthenticationState();

// Register purely frontend UI state for backward compatibility if referenced
builder.Services.AddScoped<FrontendUserState>();

// Register shopping cart state
builder.Services.AddScoped<CartService>();

// Configure Entity Framework Core for PostgreSQL / Neon.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Action<DbContextOptionsBuilder> configureDb = options =>
{
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseNpgsql("Host=localhost;Database=pizzapp_dev;Username=postgres;Password=postgres");
    }
};

builder.Services.AddDbContext<PizzAppDbContext>(configureDb, ServiceLifetime.Scoped, ServiceLifetime.Singleton);
builder.Services.AddDbContextFactory<PizzAppDbContext>(configureDb);

// Configure ASP.NET Core Identity with intermediate/student-friendly settings
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PizzAppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Run database migrations and seed default menu items, roles, and test users
try
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<PizzAppDbContext>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await DbInitializer.SeedAsync(db, roleManager, userManager);
}
catch (Exception ex)
{
    app.Logger.LogWarning(
        ex,
        "Could not initialize database. The application will continue, but database-backed features may be unavailable.");
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

// Authenticate before authorization so protected pages receive the user's role claims.
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

// Map HTTP POST authentication endpoints (Login, Register, Logout)
app.MapAuthEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
