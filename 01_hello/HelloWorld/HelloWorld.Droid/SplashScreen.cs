using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace HelloWorld.Droid
{
    [Activity (
        Label = "HelloWorld.Droid"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen ()
            : base (Resource.Layout.SplashScreen)
        {
             
        }


        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);
         
        }
    }
}
