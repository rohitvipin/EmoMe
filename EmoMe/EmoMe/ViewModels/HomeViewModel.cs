using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Entities;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels.Interfaces;
using EmoMe.Common.Enums;
using EmoMe.Models;
using EmoMe.Views.Interfaces;
using Microsoft.Practices.Unity;
using Plugin.Media.Abstractions;

namespace EmoMe.ViewModels
{
    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ILoggingService _loggingService;
        private readonly IUnityContainer _unityContainer;
        private readonly INavigationService _navigationService;
        private readonly IMediaService _mediaService;
        private readonly IDataAccessService _dataAccessService;

        private ObservableCollection<ImageEntity> _imageDetails;

        public HomeViewModel(IDialogService dialogService
            , ILoggingService loggingService
            , IUnityContainer unityContainer
            , INavigationService navigationService
            , IMediaService mediaService
            , IDataAccessService dataAccessService) : base(dialogService, loggingService)
        {
            _dialogService = dialogService;
            _loggingService = loggingService;
            _unityContainer = unityContainer;
            _navigationService = navigationService;
            _mediaService = mediaService;
            _dataAccessService = dataAccessService;

            TakePhotoCommand = new AsyncRelayCommand(TakePhotoCommandHandler);
            PickPhotoCommand = new AsyncRelayCommand(PickPhotoCommandHandler);
            FilterPhotoListCommand = new AsyncRelayCommand(FilterPhotoListCommandHandler);
        }

        public override string PageTitle => PageTitles.ApplicationTitle;

        public ObservableCollection<ImageEntity> ImageDetails
        {
            get { return _imageDetails; }
            set
            {
                _imageDetails = value;
                OnPropertyChanged();
            }
        }

        public AsyncRelayCommand TakePhotoCommand { get; }

        public AsyncRelayCommand PickPhotoCommand { get; }

        public AsyncRelayCommand FilterPhotoListCommand { get; }

        private async Task FilterPhotoListCommandHandler()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                BeginBusy();
                var imageFilterView = _unityContainer.Resolve<IImageFilterView>();
                await _navigationService.NavigateFromRootPage(imageFilterView, PageTitles.FilterTitle);
                await imageFilterView.Initialize();
            }
            catch (Exception ex)
            {
                _dialogService.ShowToastMessage(PageTitle, Messages.FilterError, ToastNotificationType.Error);
                _loggingService.Error(ex);
            }
            finally
            {
                EndBusy();
            }
        }

        private async Task PickPhotoCommandHandler()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                if (!_mediaService.IsPickPhotoSupported)
                {
                    _dialogService.ShowToastMessage(PageTitle, Messages.PhotosNotSupported, ToastNotificationType.Error);
                    return;
                }

                using (var file = await _mediaService.PickPhotoAsync())
                {
                    if (file == null)
                    {
                        return;
                    }

                    await PostSelectImageActions(file.Path, file);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowToastMessage(PageTitle, Messages.GalleryError, ToastNotificationType.Error);
                _loggingService.Error(ex);
            }
        }

        private async Task TakePhotoCommandHandler()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                if (_mediaService.IsCameraAvailable && _mediaService.IsTakePhotoSupported)
                {
                    // Take a photo
                    using (var file = await _mediaService.TakePhotoAsync())
                    {
                        await PostSelectImageActions(file.Path, file);
                    }
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowToastMessage(PageTitle, Messages.CameraErrorMessage, ToastNotificationType.Error);
                _loggingService.Error(exception);
            }
        }

        /// <summary>
        /// Inserts the image details into the database and navigate to the detail page. 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        private async Task PostSelectImageActions(string imagePath, MediaFile imageFile)
        {
            try
            {
                //insert file to database
                var imageId = _dataAccessService.InsertImageDetails(new ImageModel { Path = imagePath });
                var imageDetailView = _unityContainer.Resolve<IImageDetailView>();
                await _navigationService.NavigateFromRootPage(imageDetailView, PageTitles.ImageDetailTitle);
                await imageDetailView.Initialize(imageId, imageFile);
            }
            catch (Exception exception)
            {
                _dialogService.ShowToastMessage(PageTitle, Messages.UnExpectedError, ToastNotificationType.Error);
                _loggingService.Error(exception);
            }

        }

        public override async Task Initialize()
        {
            try
            {
                BeginBusy();

                ImageDetails = new ObservableCollection<ImageEntity>(_dataAccessService.GetImageDetails()
                    ?.Select(x => new ImageEntity(_navigationService, _unityContainer, _loggingService)
                    {
                        Path = x.Path,
                        Description = x.Description,
                        Title = x.Title,
                        ImageId = x.Id
                    }));

            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
                _dialogService.ShowToastMessage(PageTitle, exception.Message, ToastNotificationType.Error);
            }
            finally
            {
                EndBusy();
            }
        }
    }
}