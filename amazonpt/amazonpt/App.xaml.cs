using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace amazonpt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            OneSignal.Current.StartInit("02fed716-2742-4d0e-b796-4d9b86c3dafb")
           .Settings(new Dictionary<string, bool>() {
                { IOSSettings.kOSSettingsKeyAutoPrompt, false },
                { IOSSettings.kOSSettingsKeyInAppLaunchURL, false } })
           .InFocusDisplaying(OSInFocusDisplayOption.Notification)
           .EndInit();

            if (!Application.Current.Properties.ContainsKey("PlayerId"))
            {
                OneSignal.Current.IdsAvailable(IdsAvaliable);
            }
        }

        private void IdsAvaliable(string playerId, string pushToken)
        {
            Application.Current.Properties.Add("PlayerId", playerId);
            Application.Current.Properties.Add("PushToken", pushToken);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
