using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Common.Enums;
using EmoMe.Entities;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels.Interfaces;
using Microsoft.Practices.Unity;

namespace EmoMe.ViewModels
{
    public class ImageFilterViewModel : BaseViewModel, IImageFilterViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;
        private readonly ILoggingService _loggingService;
        private readonly IDialogService _dialogService;

        public ImageFilterViewModel(INavigationService navigationService, IUnityContainer unityContainer, ILoggingService loggingService, IDialogService dialogService) : base(dialogService, loggingService)
        {
            _navigationService = navigationService;
            _unityContainer = unityContainer;
            _loggingService = loggingService;
            _dialogService = dialogService;

            DoneCommand = new AsyncRelayCommand(DoneCommandHandler);
        }

        private async Task DoneCommandHandler()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                BeginBusy();

                await _navigationService.GoBack(false);
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

        private ObservableCollection<ImageFilterEnity> _imageFilterEnities;

        public override string PageTitle => PageTitles.FilterTitle;

        public override async Task Initialize()
        {
            ImageFilterEnities = new ObservableCollection<ImageFilterEnity>
            {
                new ImageFilterEnity { FilterName = "Age"},
                new ImageFilterEnity { FilterName = "Gender"},
                new ImageFilterEnity { FilterName = "Pose"},
                new ImageFilterEnity { FilterName = "Smile"},
                new ImageFilterEnity { FilterName = "Facial Hair"},
                new ImageFilterEnity { FilterName = "Glasses"},
                new ImageFilterEnity { FilterName = "anger"},
                new ImageFilterEnity { FilterName = "contempt"},
                new ImageFilterEnity { FilterName = "disgust"},
                new ImageFilterEnity { FilterName = "fear"},
                new ImageFilterEnity { FilterName = "happiness"},
                new ImageFilterEnity { FilterName = "neutral"},
                new ImageFilterEnity { FilterName = "sadness"},
                new ImageFilterEnity { FilterName = "surprise"}
            };
        }

        public ObservableCollection<ImageFilterEnity> ImageFilterEnities
        {
            get { return _imageFilterEnities; }
            set
            {
                _imageFilterEnities = value;
                OnPropertyChanged();
            }
        }

        public AsyncRelayCommand DoneCommand { get; }
    }
}
