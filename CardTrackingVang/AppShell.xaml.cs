namespace CardTrackingVang
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Main", typeof(MainPage));
            Routing.RegisterRoute("CardHandling", typeof(CardHandling));
            Routing.RegisterRoute("AddCard", typeof(AddCard));
            Routing.RegisterRoute("RemoveCard", typeof(DeleteCard));
            Routing.RegisterRoute("CardDetails", typeof(CardDetails));
        }
    }
}
