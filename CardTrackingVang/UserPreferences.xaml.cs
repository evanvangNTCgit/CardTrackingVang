namespace CardTrackingVang;

public partial class UserPreferences : ContentPage
{
    public UserPreferences()
    {
        InitializeComponent();
    }

    private async void UserThemeToggle_Toggled(object sender, ToggledEventArgs e)
    {
        try
        {
            if (Application.Current.Resources.ContainsKey("UserBgColor"))
            {
                Application.Current.Resources.TryGetValue("UserBgColor", out object Vang);
                Color userColor = (Color)Vang;

                if (userColor.ToHex() == "#000000")
                {
                    Application.Current.Resources["UserBgColor"] = Color.FromArgb("#FDFFFC");
                    LoadingUserPreferences.setUserBgTheme(Color.FromArgb("#FDFFFC"));
                }
                else
                {
                    Application.Current.Resources["UserBgColor"] = Color.FromArgb("#000000");
                    LoadingUserPreferences.setUserBgTheme(Color.FromArgb("#000000"));
                }
            }
        }
        catch (Exception ex)
        {
            // Its okay no need to set a theme just notify user although.
            await DisplayAlertAsync("ALERT", $"Failed to set a different background theme\n\nPlease notify Mr.Vang of issue\n\n{ex.Message}", "OK");
        }
    }

    private async void AnimationsToggle_Toggled(object sender, ToggledEventArgs e)
    {
        try
        {
            bool isAnimationEnabled = LoadingUserPreferences.GetUserAnimationPreference();
            LoadingUserPreferences.setUserAnimationPreference(!isAnimationEnabled);
            this.AnimationsIsToggled.Text = LoadingUserPreferences.GetUserAnimationPreference() ? "On" : "Off";
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("ALERT", $"Failed to set animation preference please contact Mr.Vang\nP{ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        bool isAnimationEnabled = LoadingUserPreferences.GetUserAnimationPreference(); // Returns true for fallback case.. no null
        this.AnimationsToggle.IsToggled = isAnimationEnabled;
        this.AnimationsIsToggled.Text = isAnimationEnabled ? "On" : "Off";
    }
}