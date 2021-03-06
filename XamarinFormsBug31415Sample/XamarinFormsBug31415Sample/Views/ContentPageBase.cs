using System;
using System.Reactive.Disposables;
using ReactiveUI.XamForms;

namespace XamarinFormsBug31415Sample.Views
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            SubscriptionDisposables.Clear();
        }
    }
}

