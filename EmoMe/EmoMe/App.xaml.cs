using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Repositories;
using EmoMe.Repositories.Interfaces;
using EmoMe.Services;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Views;
using EmoMe.Views.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace EmoMe
{
    public partial class App : Application
    {
        public App()
        {
            var unityContainer = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

            RegisterTypes(unityContainer);

            var navigationService = unityContainer.Resolve<INavigationService>();
            navigationService.AppRoot = this;

            var homeView = unityContainer.Resolve<IHomeView>();
            Task.Run(async () => await homeView.Initialize());
            navigationService.SetRoot(homeView, PageTitles.ApplicationTitle);
        }

        private static void RegisterTypes(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                return;
            }

            //Views
            unityContainer.RegisterType<IHomeView, HomeView>();
            unityContainer.RegisterType<IImageDetailView, ImageDetailView>();
            unityContainer.RegisterType<IImageFilterView, ImageFilterView>();

            //ViewModels
            unityContainer.RegisterType<IHomeViewModel, HomeViewModel>();
            unityContainer.RegisterType<IImageDetailViewModel, ImageDetailViewModel>();
            unityContainer.RegisterType<IImageFilterViewModel, ImageFilterViewModel>();

            //Services
            unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<ILoggingService, LoggingService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IMediaService, MediaService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IImageDetailRepository, ImageDetailRepository>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IDataAccessService, DataAccessService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IFaceService, FaceService>(new ContainerControlledLifetimeManager());
        }
    }
}
