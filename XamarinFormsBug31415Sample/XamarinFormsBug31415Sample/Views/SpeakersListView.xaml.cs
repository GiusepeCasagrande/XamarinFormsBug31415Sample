using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using XamarinFormsBug31415Sample.ViewModels;
using ReactiveUI;
using Xamarin.Forms;

namespace XamarinFormsBug31415Sample.Views
{
    public partial class SpeakersListView : ContentPageBase<SpeakersListViewModel>
    {
        public SpeakersListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, x => x.Speakers, x => x.SpeakersList.ItemsSource).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.LoadSpeakers, x => x.SpeakersList.RefreshCommand).DisposeWith(SubscriptionDisposables);
                this.OneWayBind(ViewModel, x => x.IsRefreshing, x => x.SpeakersList.IsRefreshing).DisposeWith(SubscriptionDisposables);
                this.Bind(ViewModel, x => x.SearchTerm, x => x.SearchBar.Text).DisposeWith(SubscriptionDisposables);
            });
        }
    }
}
