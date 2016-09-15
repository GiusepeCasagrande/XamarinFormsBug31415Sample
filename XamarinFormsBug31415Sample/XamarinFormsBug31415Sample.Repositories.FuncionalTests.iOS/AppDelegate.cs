using Foundation;
using UIKit;

namespace XamarinFormsBug31415Sample.Repositories.FuncionalTests.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

            //Cache.Initialize("AppName");

            var nunit = new NUnit.Runner.App
            {
                AutoRun = false
            };
            nunit.AddTestAssembly(typeof(SampleTest).Assembly);
            LoadApplication(nunit);

            return base.FinishedLaunching(application, launchOptions);
        }

        public override void WillTerminate(UIApplication uiApplication)
        {
            base.WillTerminate(uiApplication);
            //Cache.Shutdown();
        }
    }
}


