using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.ViewModel;
using System.Runtime.CompilerServices;

namespace CardTrackingVang;

public partial class CardHandling : ContentPage
{
	private CardsListViewModel _cardListViewModel;
	public CardHandling(CardsListViewModel cm)
	{
		this._cardListViewModel = cm;

		InitializeComponent();

        // Bind the UI element to the current list of card types
        this.TestOutput.ItemsSource = this._cardListViewModel.Cards;
    }

    private async void AddCardBtn_Clicked(object sender, EventArgs e)
    {
		// Navigate to add card page.
		await DisplayAlertAsync("Add card", "Implementing add page soon", "OK");
    }
}