using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using XamarinFormsBug31415Sample.Domain.Schedule;
using XamarinFormsBug31415Sample.Utils;
using ReactiveUI;
using Skahal.Infrastructure.Framework.Repositories;
using Splat;

namespace XamarinFormsBug31415Sample.ViewModels
{
    public class SpeakersListViewModel : ViewModelBase
    {
        public ReactiveList<Speaker> Speakers
        {
            get;
        }

        string _SearchTerm;
        public string SearchTerm
        {
            get { return _SearchTerm; }
            set { this.RaiseAndSetIfChanged(ref _SearchTerm, value); }
        }


        ObservableAsPropertyHelper<bool> m_isRefreshing;
        public bool IsRefreshing => m_isRefreshing.Value;

        public ReactiveCommand<string, IEnumerable<Speaker>> LoadSpeakers
        {
            get;
            protected set;
        }

        public ReactiveCommand<string, IEnumerable<Speaker>> SearchCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<string, IEnumerable<Speaker>> ExecuteSearch
        {
            get;
            protected set;
        }


        SpeakerService m_service;
        ISpeakerRepository m_repository;
        IUnitOfWork m_unitOfWork;

        public SpeakersListViewModel(IScreen hostScreen = null, ISpeakerRepository speakerRepository = null, IUserDialogsService userDialogsService = null) : base(hostScreen, userDialogsService)
        {
            UrlPathSegment = "Speakers";

            m_repository = speakerRepository ?? Locator.Current.GetService<ISpeakerRepository>();
            m_unitOfWork = m_unitOfWork ?? Locator.Current.GetService<IUnitOfWork>();
            m_service = new SpeakerService(m_repository);

            var canSearch = this.WhenAny(x => x.SearchTerm, x => !String.IsNullOrWhiteSpace(x.Value));
            var canLoadAll = this.WhenAny(x => x.SearchTerm, x => String.IsNullOrWhiteSpace(x.Value));

            Speakers = new ReactiveList<Speaker>();
            LoadSpeakers = ReactiveCommand.CreateFromTask<string, IEnumerable<Speaker>>(_ => m_service.GetAllSpeakersAsync(), canLoadAll);
            LoadSpeakers
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(list => AddSpeakersToList(list))
                .DisposeWith(subscriptionDisposables);


            ExecuteSearch = ReactiveCommand.CreateFromTask<string, IEnumerable<Speaker>>((s) => m_service.GetSpeakersByNameAsync(SearchTerm), canSearch);
            ExecuteSearch
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(list => AddSpeakersToList(list))
                .DisposeWith(subscriptionDisposables);

            m_isRefreshing = LoadSpeakers
                                .IsExecuting
                                .CombineLatest(ExecuteSearch.IsExecuting, (a, b) => a || b).DistinctUntilChanged()
                                .Select(x => x)
                                .ToProperty(this, x => x.IsRefreshing, true)
                                .DisposeWith(subscriptionDisposables);

            this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(400), RxApp.MainThreadScheduler)
                .Select(x => x?.Trim())
                .DistinctUntilChanged()
                .Where(x => String.IsNullOrWhiteSpace(x))
                .InvokeCommand(LoadSpeakers);

            this
                .WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(800), RxApp.MainThreadScheduler)
                .Select(x => x?.Trim())
                .DistinctUntilChanged()
                .Where(x => !String.IsNullOrWhiteSpace(x))
                .InvokeCommand(ExecuteSearch);


            Observable
                .Merge(ExecuteSearch.ThrownExceptions, LoadSpeakers.ThrownExceptions)
                .SubscribeOn(RxApp.MainThreadScheduler)
                .Subscribe(ex =>
                {
                    Dialogs.ShowError($"Error: {ex.InnerException.ToString()}");
                })
               .DisposeWith(subscriptionDisposables);
        }

        void AddSpeakersToList(IEnumerable<Speaker> list)
        {
            Speakers.Clear();
            Speakers.AddRange(list);
        }
    }
}
