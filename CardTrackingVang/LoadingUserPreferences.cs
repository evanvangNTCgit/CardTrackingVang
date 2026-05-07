namespace CardTrackingVang
{
    public static class LoadingUserPreferences
    {
        public static bool loadedStartup = false;

        public static void setUserBgTheme(Color c)
        {
            try
            {
                if (c.ToHex() == "#000000")
                {
                    Preferences.Default.Set("BgTheme", "#000000");
                }
                else
                {
                    Preferences.Default.Set("BgTheme", "#FDFFFC");
                }
            }
            catch
            {
                // All good if user really wants to they could keep configuring on start up.
            }
        }

        public static void setUserAnimationPreference(bool b)
        {
            try
            {
                if (b)
                {
                    Preferences.Default.Set("Animations", true);
                } else
                {
                    Preferences.Default.Set("Animations", false);
                }
            } catch
            {
                // All good if user really does not like animations user can toggle animations off on their phone.
                // A popular method I remember being turning on low power mode.
            }
        }

        public static bool GetUserAnimationPreference()
        {
            return Preferences.Default.Get("Animations", true);
        }

        public async static void LoadPreferencesStartup()
        {
            var currentThemePreference = Preferences.Default.Get("BgTheme", "#000000");


            if (currentThemePreference == "#000000")
            {
                Application.Current!.Resources["UserBgColor"] = Color.FromArgb("#000000");
            }
            else
            {

                Application.Current!.Resources["UserBgColor"] = Color.FromArgb("#FDFFFC");
            }
        }
    }
}
