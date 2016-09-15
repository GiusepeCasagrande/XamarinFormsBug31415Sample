using FreshMvvm;
using ReactiveUI;
using Xamarin.Forms;

namespace XamarinFormsBug31415Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var bootstrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();
            MainPage = bootstrapper.CreateMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

