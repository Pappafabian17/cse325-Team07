namespace PizzApp.Services;

public class FrontendUserState
{
    public string CurrentRole { get; private set; } = "Guest";
    public string UserName { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;
    public bool IsAuthenticated => CurrentRole != "Guest";

    public event Action? OnChange;

    public void SetRole(string role)
    {
        CurrentRole = role switch
        {
            "Customer" => "Customer",
            "Staff" => "Staff",
            "Administrator" => "Administrator",
            _ => "Guest"
        };

        switch (CurrentRole)
        {
            case "Customer":
                UserName = "Alex Customer";
                UserEmail = "customer@pizzapp.com";
                break;
            case "Staff":
                UserName = "Sam Kitchen Staff";
                UserEmail = "staff@pizzapp.com";
                break;
            case "Administrator":
                UserName = "Jordan Administrator";
                UserEmail = "admin@pizzapp.com";
                break;
            default:
                UserName = string.Empty;
                UserEmail = string.Empty;
                break;
        }

        OnChange?.Invoke();
    }

    public void Logout() => SetRole("Guest");
}
