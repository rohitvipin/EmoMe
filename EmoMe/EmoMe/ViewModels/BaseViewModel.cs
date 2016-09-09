using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EmoMe.Common;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels.Interfaces;
using Xamarin.Forms;

namespace EmoMe.ViewModels
{
    public abstract class BaseViewModel : IViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ILoggingService _loggingService;
        private int _busyTaskCount;

        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseViewModel(IDialogService dialogService, ILoggingService loggingService)
        {
            _dialogService = dialogService;
            _loggingService = loggingService;
        }

        public BaseViewModel()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
            }
        }

        public bool IsBusy => _busyTaskCount != 0;

        public bool IsActionsAllowed => _busyTaskCount == 0;

        public void BeginBusy()
        {
            _busyTaskCount++;
            if (IsBusy)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _dialogService.ShowLoading(Messages.LoadingMessage, MaskType.Black);
                });
            }
        }

        public void EndBusy()
        {
            if (_busyTaskCount > 0)
            {
                _busyTaskCount--;
            }
            if (IsBusy == false)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _dialogService.HideLoading();
                });
            }
        }

        public abstract string PageTitle { get; }

        public abstract Task Initialize();
    }
}