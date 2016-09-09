using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace EmoMe.Droid
{
    [Activity(Label = "EmoMe", Icon = "@drawable/icon", Theme = "@style/MyCustomTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

            ActionBar.SetIcon(Color.Transparent.ToAndroid());

            CachedImageRenderer.Init();

            RegisterThirdPartyControls();

            LoadApplication(new App());

        }

        private void RegisterThirdPartyControls()
        {
            UserDialogs.Init(this);
        }
    }
}

