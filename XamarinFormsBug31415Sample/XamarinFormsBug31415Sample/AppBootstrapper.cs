using ReactiveUI;
using ReactiveUI.XamForms;
using XamarinFormsBug31415Sample.Infrastructure.Repositories.Memory;
using XamarinFormsBug31415Sample.ViewModels;
using XamarinFormsBug31415Sample.Views;
using Skahal.Infrastructure.Framework.Repositories;
using Splat;
using Xamarin.Forms;
using XamarinFormsBug31415Sample.Domain.Schedule;
using XamarinFormsBug31415Sample.Utils;

namespace XamarinFormsBug31415Sample
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        IUnitOfWork m_unitOfWork;

        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            InitializeMemoryRepositories();

            Locator.CurrentMutable.RegisterLazySingleton(() => new UserDialogsService(), typeof(IUserDialogsService));
            Locator.CurrentMutable.Register(() => new SpeakersListView(), typeof(IViewFor<SpeakersListViewModel>));

            Router.Navigate.Execute(new SpeakersListViewModel(this));
        }

        void InitializeMemoryRepositories()
        {
            m_unitOfWork = new MemoryUnitOfWork();
            Locator.CurrentMutable.RegisterConstant(m_unitOfWork, typeof(IUnitOfWork));
            Locator.CurrentMutable.RegisterConstant(new MemorySpeakerRepository(m_unitOfWork), typeof(ISpeakerRepository));

            CreateStubData();
        }

        #region stubData
        void CreateStubData()
        {
            var unitOfWork = Locator.Current.GetService<IUnitOfWork>();

            var speakerRepository = Locator.Current.GetService<ISpeakerRepository>();

            var loreIpsum = "Lorem Ipsum é simplesmente uma simulação de texto da indústria tipográfica e de impressos, e vem sendo utilizado desde o século XVI, quando um impressor desconhecido pegou uma bandeja de tipos e os embaralhou para fazer um livro de modelos de tipos. Lorem Ipsum sobreviveu não só a cinco séculos, como também ao salto para a editoração eletrônica, permanecendo essencialmente inalterado. Se popularizou na década de 60, quando a Letraset lançou decalques contendo passagens de Lorem Ipsum, e mais recentemente quando passou a ser integrado a softwares de editoração eletrônica como Aldus PageMaker.";
            var profileImage = "https://x1.xingassets.com/assets/frontend_minified/img/users/nobody_m.original.jpg";

            speakerRepository.Attach(new Speaker
            {
                Id = "1",
                Name = "Primeiro",
                Bio = loreIpsum,
                ProfilePictureUrl = profileImage
            });

            speakerRepository.Attach(new Speaker
            {
                Id = "2",
                Name = "Segundo",
                Bio = loreIpsum,
                ProfilePictureUrl = profileImage
            });

            speakerRepository.Attach(new Speaker
            {
                Id = "3",
                Name = "Terceiro",
                Bio = loreIpsum,
                ProfilePictureUrl = profileImage
            });

            speakerRepository.Attach(new Speaker
            {
                Id = "4",
                Name = "Quarto",
                Bio = loreIpsum,
                ProfilePictureUrl = profileImage
            });

            speakerRepository.Attach(new Speaker
            {
                Id = "5",
                Name = "Quinto",
                Bio = loreIpsum,
                ProfilePictureUrl = profileImage
            });

            unitOfWork.CommitAsync();
        }
        #endregion

        public Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapper as an IScreen.
            return new RoutedViewHost();
        }
    }
}