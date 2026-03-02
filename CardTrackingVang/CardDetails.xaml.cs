namespace CardTrackingVang;

[QueryProperty(nameof(Title), "title")]
[QueryProperty(nameof(Value), "value")]
[QueryProperty(nameof(CardType), "cardtype")]
public partial class CardDetails : ContentPage
{
    private string title;
	private decimal value;
	private string cardtype;

	public CardDetails()
	{
		InitializeComponent();

		BindingContext = this;
	}

    // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-10.0#receive-navigation-data
    public string Title
	{
		get => this.title;
		set
		{
			this.title = value;
			OnPropertyChanged();
		}
	}

	public decimal Value
	{
		get => this.value;
		set
		{
			this.value = value;
			OnPropertyChanged();
		}
	}

	public string CardType
	{
		get => this.cardtype;
		set
		{
			this.cardtype = value;
			OnPropertyChanged();
		}
	}
}