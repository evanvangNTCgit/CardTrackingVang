namespace CardTrackingVang.Models;

public class Card
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public decimal Value { get; set; } = 0.00m;

    public int CardTypeID { get; set; }
    public CardType CardType { get; set; }

    public int CardImageId { get; set; }
    public CardImage CardImage { get; set; }
}
