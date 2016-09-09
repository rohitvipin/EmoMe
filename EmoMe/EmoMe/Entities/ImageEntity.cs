using System;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Views.Interfaces;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace EmoMe.Entities
{
    public class ImageEntity : BaseEntity
    {
        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;
        private readonly ILoggingService _loggingService;

        private string _path;

        public ImageEntity(INavigationService navigationService, IUnityContainer unityContainer, ILoggingService loggingService)
        {
            _navigationService = navigationService;
            _unityContainer = unityContainer;
            _loggingService = loggingService;

            SelectImageDetailCommand = new AsyncRelayCommand(SelectImageDetailCommandHandler);
        }

        private async Task SelectImageDetailCommandHandler()
        {
            if (_navigationService == null || _unityContainer == null)
            {
                return;
            }

            var listImageViewModel = _unityContainer.Resolve<IImageDetailViewModel>();

            if (listImageViewModel.IsBusy)
            {
                return;
            }

            try
            {
                listImageViewModel.BeginBusy();
                var detailsImagelView = _unityContainer.Resolve<IImageDetailView>();
                await _navigationService.NavigateFromRootPage(detailsImagelView, PageTitles.ImageDetailTitle);
                await detailsImagelView.Initialize(ImageId, null);
            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
            }
            finally
            {
                listImageViewModel.EndBusy();
            }
        }

        public int ImageId { get; set; }

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public ImageSource ImageSource => ImageSource.FromFile(Path);

        public AsyncRelayCommand SelectImageDetailCommand { get; }
    }
}