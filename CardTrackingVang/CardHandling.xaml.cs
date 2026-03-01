namespace CardTrackingVang;

public partial class CardHandling : ContentPage
{
	public CardHandling()
	{
		InitializeComponent();
	}

    private async void AddCardBtn_Clicked(object sender, EventArgs e)
    {
		// Navigate to add card page.
		await DisplayAlertAsync("Add card", "Implementing add page soon", "OK");
    }
}