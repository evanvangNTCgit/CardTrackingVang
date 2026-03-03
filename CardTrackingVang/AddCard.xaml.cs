using CardTrackingVang.Models;
using CardTrackingVang.ViewModel;

namespace CardTrackingVang;

public partial class AddCard : ContentPage
{
    CardsListViewModel _cardListViewModel;

    List<CardType> _cardTypes;

    public AddCard(CardsListViewModel clvm)
    {
        this._cardListViewModel = clvm;

        InitializeComponent();

        // We need a list of string to set the picker items for card types.
        this._cardTypes = this._cardListViewModel.GetCardTypes();
        this.CardTypePicker.ItemsSource = this._cardTypes.Select(ct => ct.Type).ToList();
    }

    private async void OnAddCardClicked(object sender, EventArgs e)
    {
        try
        {
            // Hold on lets check if user already added the card and verify if the duplication is intentional.
            var existingCard = this._cardListViewModel.Cards.FirstOrDefault(c => c.Title == this.TitleEntry.Text && c.CardTypeID == this._cardTypes.First(ct => ct.Type == this.CardTypePicker.SelectedItem.ToString()).Id);
            if (existingCard != null)
            {
                bool answer = await DisplayAlertAsync("ALERT",
                    $"A card with the title '{existingCard.Title}' and type '{this.CardTypePicker.SelectedItem}' already exists. Do you want to add another one?",
                    "Yes",
                    "No");

                if (!answer)
                {
                    // Take the user back...
                    await Shell.Current.GoToAsync("..");
                }
            }

            // Make the card model...
            var cardGettingAdded = new Card
            {
                Title = this.TitleEntry.Text,
                Value = decimal.Parse(this.ValueEntry.Text),
                CardTypeID = this._cardTypes.First(ct => ct.Type == this.CardTypePicker.SelectedItem.ToString()).Id
            };

            this._cardListViewModel.AddCardWithModel(cardGettingAdded);

            await DisplayAlertAsync("Success", "Card added successfully!\nTaking you back...", "OK");
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            // Failed to add card notify user of error and a soon to be fixed.
            await DisplayAlertAsync("ALERT", $"Failed to add card. Please try again later.\n\nError details: {ex.Message}", "OK");
            await DisplayAlertAsync("ALERT", "Taking you back...", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}