using CardTrackingVang.ViewModel;

namespace CardTrackingVang
{
    public partial class MainPage : ContentPage
    {
        private CardsListViewModel _cardListVM;

        public MainPage(CardsListViewModel cm)
        {
            this._cardListVM = cm;

            InitializeComponent();

            // Bind the UI element to the current list of card types
            this.TestOutput.ItemsSource = this._cardListVM.Cards;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

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
