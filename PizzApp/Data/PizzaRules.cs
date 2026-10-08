namespace PizzApp.Data;

public static class PizzaRules
{
    public static readonly string[] Sizes =
    [
        "Small",
        "Medium",
        "Large",
        "X-Large"
    ];

    // Keep builder limits and base prices centralized so editing and reordering apply the same rules.
    public static int MaxToppings(string size) => size switch
    {
        "Small" => 4,
        "Medium" => 6,
        "Large" => 8,
        "X-Large" => 10,
        _ => 0
    };

    public static decimal BasePriceFor(string size) => size switch
    {
        "Small" => 8.00m,
        "Medium" => 10.00m,
        "Large" => 12.00m,
        "X-Large" => 14.00m,
        _ => 0m
    };

    public static bool IsValidSize(string size)
    {
        return Sizes.Contains(size);
    }
}
