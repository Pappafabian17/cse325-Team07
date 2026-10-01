using Microsoft.AspNetCore.Identity;

namespace PizzApp.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public string? DeliveryAddress { get; set; }
}
