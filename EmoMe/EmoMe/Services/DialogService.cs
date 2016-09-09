using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EmoMe.Common.Enums;
using EmoMe.Services.Interfaces;
using Color = System.Drawing.Color;

namespace EmoMe.Services
{
    public class DialogService : IDialogService
    {
        private const string OkButtonText = "OK";
        private readonly IUserDialogs _userDialogs = UserDialogs.Instance;
        private readonly ILoggingService _loggingService;

        public DialogService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public async Task ShowError(string title, string message, string buttonText, Action afterHideCallback) => await ShowMessage(title, message, buttonText, afterHideCallback);

        public async Task ShowError(string title, Exception error, string buttonText, Action afterHideCallback) => await ShowMessage(title, error.Message, buttonText, afterHideCallback);

        public async Task ShowMessage(string title, string message) => await ShowMessage(title, message, OkButtonText, null);

        public async Task ShowMessage(string title, string message, Action afterHideCallback) => await ShowMessage(title, message, OkButtonText, afterHideCallback);

        public void ShowToastMessage(string title, string message, ToastNotificationType toastNotificationType)
        {
            try
            {
                Color toastbackgroundColor;
                switch (toastNotificationType)
                {
                    case ToastNotificationType.Success:
                        toastbackgroundColor = Color.Green;
                        break;
                    case ToastNotificationType.Error:
                        toastbackgroundColor = Color.Red;
                        break;
                    case ToastNotificationType.Warning:
                        toastbackgroundColor = Color.Orange;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(toastNotificationType), toastNotificationType, null);
                }

                _userDialogs.Toast(new ToastConfig(title)
                {
                    Action = null,
                    BackgroundColor = toastbackgroundColor,
                    Duration = TimeSpan.FromSeconds(3),
                    Message = message,
                    MessageTextColor = Color.White
                });
            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
            }
        }

        public async Task ShowMessage(string title, string message, string buttonText, Action afterHideCallback)
        {
            await _userDialogs.AlertAsync(message, title, buttonText);

            afterHideCallback?.Invoke();
        }

        public async Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback = null)
        {
            var result = await _userDialogs.ConfirmAsync(message, title, buttonConfirmText, buttonCancelText);

            afterHideCallback?.Invoke(result);

            return result;
        }

        public async Task<string> ShowActionSheet(string title, string cancel, string destruction, CancellationToken cancellationToken, Action<string> afterHideCallback, params string[] buttons)
        {
            var result = await _userDialogs.ActionSheetAsync(title, cancel, destruction, cancellationToken, buttons);
            afterHideCallback?.Invoke(result);

            return result;
        }

        public void ShowLoading(string title, MaskType maskType)
        {
            try
            {
                _userDialogs.ShowLoading(title, maskType);
            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
            }
        }

        public void HideLoading()
        {
            try
            {
                _userDialogs.HideLoading();
            }
            catch (Exception exception)
            {
                _loggingService.Error(exception);
            }
        }
    }
}