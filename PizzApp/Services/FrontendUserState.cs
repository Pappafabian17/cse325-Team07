namespace PizzApp.Services;

public class FrontendUserState
{
    public string CurrentRole { get; private set; } = "Guest";
    public string StatusMessage { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string UserEmail { get; private set; } = string.Empty;
    public bool IsAuthenticated => CurrentRole != "Guest";

    private string RegisteredName { get; set; } = string.Empty;
    private string RegisteredEmail { get; set; } = string.Empty;
    private string RegisteredPassword { get; set; } = string.Empty;

    public event Action? OnChange;

    public bool Register(string name, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        RegisteredName = name.Trim();
        RegisteredEmail = email.Trim();
        RegisteredPassword = password;

        CurrentRole = "Customer";
        UserName = RegisteredName;
        UserEmail = RegisteredEmail;

        OnChange?.Invoke();

        return true;
    }

    public bool Login(string email, string password)
    {
        email = email.Trim();

        if (email.Equals("customer@pizzapp.com", StringComparison.OrdinalIgnoreCase)
            && password == "Customer123")
        {
            CurrentRole = "Customer";
            UserName = "Alex Customer";
            UserEmail = "customer@pizzapp.com";

            OnChange?.Invoke();
            return true;
        }

        if (email.Equals("staff@pizzapp.com", StringComparison.OrdinalIgnoreCase)
            && password == "Staff123")
        {
            CurrentRole = "Staff";
            UserName = "Sam Kitchen Staff";
            UserEmail = "staff@pizzapp.com";

            OnChange?.Invoke();
            return true;
        }

        if (email.Equals("admin@pizzapp.com", StringComparison.OrdinalIgnoreCase)
            && password == "Admin123")
        {
            CurrentRole = "Administrator";
            UserName = "Jordan Administrator";
            UserEmail = "admin@pizzapp.com";

            OnChange?.Invoke();
            return true;
        }

        if (!string.IsNullOrWhiteSpace(RegisteredEmail) &&
            email.Equals(RegisteredEmail, StringComparison.OrdinalIgnoreCase) &&
            password == RegisteredPassword)
        {
            CurrentRole = "Customer";
            UserName = RegisteredName;
            UserEmail = RegisteredEmail;

            OnChange?.Invoke();
            return true;
        }

        return false;
    }

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

   public void Logout()
{
    CurrentRole = "Guest";
    UserName = string.Empty;
    UserEmail = string.Empty;
    StatusMessage = "You have been logged out successfully.";

    OnChange?.Invoke();
}
}