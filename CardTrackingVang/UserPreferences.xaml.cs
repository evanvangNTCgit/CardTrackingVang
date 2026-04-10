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

                if (userColor.ToHex() == "#011627")
                {
                    Application.Current.Resources["UserBgColor"] = Color.FromArgb("#FDFFFC");
                    LoadingUserPreferences.setUserBgTheme(Color.FromArgb("#FDFFFC"));
                }
                else
                {
                    Application.Current.Resources["UserBgColor"] = Color.FromArgb("#011627");
                    LoadingUserPreferences.setUserBgTheme(Color.FromArgb("#011627"));
                }
            }
        }
        catch (Exception ex)
        {
            // Its okay no need to set a theme just notify user although.
            await DisplayAlertAsync("ALERT", $"Failed to set a different background theme\n\nPlease notify Mr.Vang of issue\n\n{ex.Message}", "OK");
        }
    }
}