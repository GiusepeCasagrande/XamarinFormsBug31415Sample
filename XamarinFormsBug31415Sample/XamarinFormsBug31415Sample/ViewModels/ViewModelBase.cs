using System;
using System.Reactive.Disposables;
using XamarinFormsBug31415Sample.Utils;
using ReactiveUI;
using Splat;

namespace XamarinFormsBug31415Sample.ViewModels
{
    public class ViewModelBase : ReactiveObject, IRoutableViewModel
    {
        public string UrlPathSegment
        {
            get;
            protected set;
        }

        public IScreen HostScreen
        {
            get;
            protected set;
        }

        protected IUserDialogsService Dialogs
        {
            get;
            set;
        }

        protected readonly CompositeDisposable subscriptionDisposables = new CompositeDisposable();

        public ViewModelBase(IScreen hostScreen = null, IUserDialogsService userDialogsService = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
            Dialogs = userDialogsService ?? Locator.Current.GetService<IUserDialogsService>();
        }
    }
}

