using CardTrackingVang.AiServices;
using CardTrackingVang.ViewModel;

namespace CardTrackingVang
{
    public partial class MainPage : ContentPage
    {
        private CardsListViewModel _cardListVM;
        private readonly AiKeys _keys;

        public MainPage(CardsListViewModel cm, AiKeys a)
        {
            this._cardListVM = cm;
            this._keys = a;

            InitializeComponent();

            // Bind the UI element to the current list of card types
            this.TestOutput.ItemsSource = this._cardListVM.Cards;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (string.IsNullOrEmpty(_keys.OpenAIEndpoint)) 
            {
                await DisplayAlertAsync("ALERT", "You have no access to AI services on this app.", "OK");
            }

            if (!LoadingUserPreferences.loadedStartup)
            {
                try
                {
                    LoadingUserPreferences.LoadPreferencesStartup();
                }
                catch (Exception ex)
                {
                    await DisplayAlertAsync("ALERT", $"Failed to load a preference\n\n{ex.Message}\n\nPlease restart and try again or notify Mr.Vang", "OK");
                }
                finally
                {
                    LoadingUserPreferences.loadedStartup = true;
                }
            }
        

        }
    }
}
