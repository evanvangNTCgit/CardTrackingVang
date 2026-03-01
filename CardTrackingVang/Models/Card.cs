namespace CardTrackingVang.Models;

public class Card
{
    public required string Title { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime? ObtainedDate { get; set; } = null;

    public decimal? Value { get; set; } = 0.00m;
}
