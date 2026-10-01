using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzApp.Models;

namespace PizzApp.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/Account").DisableAntiforgery();

        // Helper GET redirects if a user navigates directly to /Account/Login or /Account/Register
        group.MapGet("/Login", () => Results.Redirect("/login"));
        group.MapGet("/Register", () => Results.Redirect("/register"));

        // Standard Login
        group.MapPost("/Login", async (
            [FromForm] string? email,
            [FromForm] string? password,
            [FromForm] bool? rememberMe,
            [FromForm] string? returnUrl,
            SignInManager<ApplicationUser> signInManager) =>
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return Results.Redirect("/login?error=Please+provide+both+email+and+password.");
            }

            var result = await signInManager.PasswordSignInAsync(
                email.Trim(),
                password,
                isPersistent: rememberMe ?? false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var destination = !string.IsNullOrWhiteSpace(returnUrl) && returnUrl.StartsWith('/') && !returnUrl.StartsWith("//")
                    ? returnUrl
                    : "/";
                return Results.LocalRedirect(destination);
            }

            return Results.Redirect("/login?error=Invalid+email+or+password.");
        });

        // Standard Registration
        group.MapPost("/Register", async (
            [FromForm] string? fullName,
            [FromForm] string? email,
            [FromForm] string? password,
            [FromForm] string? confirmPassword,
            [FromForm] string? deliveryAddress,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) =>
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return Results.Redirect("/register?error=Please+fill+in+all+required+fields.");
            }

            if (password != confirmPassword)
            {
                return Results.Redirect("/register?error=Passwords+do+not+match.");
            }

            var existingUser = await userManager.FindByEmailAsync(email.Trim());
            if (existingUser != null)
            {
                return Results.Redirect("/register?error=An+account+with+this+email+already+exists.");
            }

            var user = new ApplicationUser
            {
                UserName = email.Trim(),
                Email = email.Trim(),
                FullName = fullName.Trim(),
                DeliveryAddress = deliveryAddress?.Trim(),
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                var errorMsg = createResult.Errors.FirstOrDefault()?.Description ?? "Failed to create account.";
                return Results.Redirect($"/register?error={Uri.EscapeDataString(errorMsg)}");
            }

            // All newly registered users receive the Customer role by default
            await userManager.AddToRoleAsync(user, "Customer");

            // Automatically sign them in
            await signInManager.SignInAsync(user, isPersistent: false);

            return Results.LocalRedirect("/");
        });

        // Logout
        group.MapPost("/Logout", async (SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.LocalRedirect("/");
        });

        // Demo Quick Login (Very helpful for students & professors testing role switching)
        group.MapPost("/QuickLogin", async (
            [FromForm] string? role,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) =>
        {
            var email = role switch
            {
                "Administrator" => "admin@pizzapp.com",
                "Staff" => "staff@pizzapp.com",
                _ => "customer@pizzapp.com"
            };

            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
            }

            return Results.LocalRedirect("/");
        });

        return app;
    }
}
