using CardTrackingVang.AiServices;
using CardTrackingVang.Services;
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
            this.TestOutput.ItemsSource = this._cardListVM.Cards;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

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
