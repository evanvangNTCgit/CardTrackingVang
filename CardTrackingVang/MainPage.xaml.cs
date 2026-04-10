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

            var test = _keys.OpenAIEndpoint;
            var testing = _keys.OpenAIKey;

            try
            {
                LoadingUserPreferences.LoadPreferencesStartup();
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("ALERT", $"Failed to load a preference\n\n{ex.Message}\n\nPlease restart and try again or notify Mr.Vang", "OK");
            }
        }
    }
}
