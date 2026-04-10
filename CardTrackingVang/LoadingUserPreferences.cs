namespace CardTrackingVang
{
    public static class LoadingUserPreferences
    {
        public static bool loadedStartup = false;

        public static void setUserBgTheme(Color c)
        {
            try
            {
                if (c.ToHex() == "#011627")
                {
                    // Preferences.Default.Set("BgTheme", Color.FromArgb("#FDFFFC"));
                    Preferences.Default.Set("BgTheme", "#011627");
                }
                else
                {
                    // Preferences.Default.Set("BgTheme", Color.FromArgb("#011627"));
                    Preferences.Default.Set("BgTheme", "#FDFFFC");
                }
            }
            catch
            {
                // All good if user really wants to they could keep configuring on start up.
            }
        }

        public async static void LoadPreferencesStartup()
        {
            var currentThemePreference = Preferences.Default.Get("BgTheme", "#011627");


            if (currentThemePreference == "#011627")
            {
                Application.Current!.Resources["UserBgColor"] = Color.FromArgb("#011627");
            }
            else
            {

                Application.Current!.Resources["UserBgColor"] = Color.FromArgb("#FDFFFC");
            }
        }
    }
}
