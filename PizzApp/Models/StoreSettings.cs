namespace PizzApp.Models;

public class StoreSettings
{
    public int Id { get; set; }

    public string OperatingHours { get; set; } =
        "11:00 AM – 10:00 PM Daily";

    public int DeliveryRadiusMiles { get; set; } = 8;

    public decimal DeliveryFee { get; set; } = 3.50m;

    public decimal SalesTaxPercent { get; set; } = 6.00m;
}