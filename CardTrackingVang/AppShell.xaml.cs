namespace CardTrackingVang
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Main", typeof(MainPage));
            Routing.RegisterRoute("AddCard", typeof(AddCard));
            Routing.RegisterRoute("CardDetails", typeof(CardDetails));
            Routing.RegisterRoute("Help", typeof(Help));
        }
    }
}
