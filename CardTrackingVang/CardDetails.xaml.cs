using CardTrackingVang.ViewModel;

namespace CardTrackingVang;

[QueryProperty(nameof(Title), "title")]
[QueryProperty(nameof(Value), "value")]
[QueryProperty(nameof(CardType), "cardtype")]
[QueryProperty(nameof(CardId), "SelectedCardId")]
public partial class CardDetails : ContentPage
{
    private string title;
    private decimal value;
    private string cardtype;
    private int cardid;

    private CardsListViewModel _cardListViewModel;

    public CardDetails(CardsListViewModel clvm)
    {
        InitializeComponent();

        this._cardListViewModel = clvm;

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
    public int CardId
    {
        get => this.cardid;
        set
        {
            this.cardid = value;
            OnPropertyChanged();
        }
    }

    private async void DeleteCardBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool answer = await DisplayAlertAsync("ALERT",
                $"Are you sure you want to delete the card '{this.Title}' of type '{this.CardType}'?",
                "Yes",
                "No");

            if (answer)
            {
                this._cardListViewModel.DeleteCard(this.CardId);

                await Shell.Current.DisplayAlertAsync("Success", "Card deleted successfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("ALERT", "Card deletion has been canceled.", "OK");
            }
        }
        catch
        {
            // Failed to delete card.. 
            // Likely already gone refresh and take user back.
            this._cardListViewModel.RefreshCards();

            // Then navigate back...
            await Shell.Current.DisplayAlertAsync("ALERT", "Failed to delete card...\nLikely already gone\nRefreshing list", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void EditValueBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            string userNewValue = await DisplayPromptAsync("Edit Card Value", $"Current value: ${this.Value}\nEnter new value:", "OK", "Cancel", "0");

            if (userNewValue != null)
            {
                this._cardListViewModel.EditCardValue(this.CardId, decimal.Parse(userNewValue));
                await Shell.Current.DisplayAlertAsync("Success", "Card value updated successfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch (FormatException)
        {
            // User entered an invalid decimal value.
            await Shell.Current.DisplayAlertAsync("ALERT", "Invalid value entered. Please enter a valid decimal number.", "OK");
        }
        catch
        {
            // Failed to update card value.. 
            this._cardListViewModel.RefreshCards();
            // Then navigate back...
            await Shell.Current.DisplayAlertAsync("ALERT", "Failed to update card value...\nRefreshing list", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void EditTitleBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            string userNewTitle = await DisplayPromptAsync("Edit Card Title", $"Current title: {this.Title}\nEnter new title:", "OK", "Cancel", this.Title);

            if (userNewTitle != null)
            {
                this._cardListViewModel.EditCardTitle(this.CardId, userNewTitle);
                await Shell.Current.DisplayAlertAsync("Success", "Card title updated successfully.", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        catch
        {
            // Failed to update card title.. 
            this._cardListViewModel.RefreshCards();
            // Then navigate back...
            await Shell.Current.DisplayAlertAsync("ALERT", "Failed to update card title...\nRefreshing list", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}