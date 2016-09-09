//using System.Threading.Tasks;
//using EmoMe.Services;
//using EmoMe.Services.Interfaces;
//using EmoMe.ViewModels;
//using EmoMe.ViewModels.Interfaces;
//using EmoMe.Views;
//using EmoMe.Views.Interfaces;
//using Microsoft.Practices.ServiceLocation;
//using Microsoft.Practices.Unity;
//using Xamarin.Forms;

//namespace EmoMe
//{
//    public class App : Application
//    {
//        public App()
//        {
//            var unityContainer = new UnityContainer();
//            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

//            RegisterTypes(unityContainer);

//            var navigationService = unityContainer.Resolve<INavigationService>();
//            navigationService.AppRoot = this;

//            var homeView = unityContainer.Resolve<IHomeView>();
//            Task.Run(async () => await homeView.Initialize());
//            navigationService.SetRoot(homeView);
//        }

//        private static void RegisterTypes(IUnityContainer unityContainer)
//        {
//            if (unityContainer == null)
//            {
//                return;
//            }

//            //Views
//            unityContainer.RegisterType<IHomeView, HomeView>();
//            unityContainer.RegisterType<IImageDetailView, ImageDetailView>();

//            //ViewModels
//            unityContainer.RegisterType<IHomeViewModel, HomeViewModel>();
//            unityContainer.RegisterType<IImageDetailViewModel, ImageDetailViewModel>();

//            //Services
//            unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
//            unityContainer.RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());
//            unityContainer.RegisterType<ILoggingService, LoggingService>(new ContainerControlledLifetimeManager());

//        }
//    }
//}
