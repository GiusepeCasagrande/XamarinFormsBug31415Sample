using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using ReactiveUI;
using UIKit;

namespace XamarinFormsBug31415Sample.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        AutoSuspendHelper suspendHelper;

        public AppDelegate()
        {
            RxApp.SuspensionHost.CreateNewAppState = () => new AppBootstrapper();
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();
            var ignore = new CircleTransformation();

            RxApp.SuspensionHost.SetupDefaultSuspendResume();

            suspendHelper = new AutoSuspendHelper(this);
            suspendHelper.FinishedLaunching(app, options);

            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            LogFonts();

            return base.FinishedLaunching(app, options);
        }

        public override void DidEnterBackground(UIApplication application)
        {
            suspendHelper.DidEnterBackground(application);
        }

        public override void OnActivated(UIApplication application)
        {
            suspendHelper.OnActivated(application);
        }

        /// <summary>
        /// Logs the installed fonts to the debug window.
        /// </summary>
        private void LogFonts()
        {
            foreach (NSString family in UIFont.FamilyNames)
            {
                foreach (NSString font in UIFont.FontNamesForFamilyName(family))
                {
                    Debug.WriteLine(@"{0}", font);
                }
            }
        }
    }
}

