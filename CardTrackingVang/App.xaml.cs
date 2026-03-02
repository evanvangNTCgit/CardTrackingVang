using CardTrackingVang.ViewModel;

namespace CardTrackingVang
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var layout = sender as BindableObject;
            var currentCard = layout?.BindingContext as CardViewModel;

            if (currentCard != null)
            {
                await Shell.Current.GoToAsync($"CardDetails?title={currentCard.Title}&value={currentCard.Value}&cardtype={currentCard.CardType.Type}");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Error", "Could not find card data.", "OK");
            }
        }
    }
}