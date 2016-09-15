using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveUI;
using Acr.UserDialogs;
using Xamarin.Forms;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Transformations;

namespace XamarinFormsBug31415Sample.Droid
{
    [Activity(Label = "XamarinFormsBug31415Sample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CachedImageRenderer.Init();
            var ignore = new CircleTransformation();

            UserDialogs.Init(() => (Activity)Forms.Context);

            RxApp.SuspensionHost.CreateNewAppState =
            () =>
            {
                Console.WriteLine("Creating app state");
                return new AppBootstrapper();
            };

            Forms.Init(this, bundle);

            RxApp.SuspensionHost.SetupDefaultSuspendResume();

            LoadApplication(new App());
        }
    }
}

