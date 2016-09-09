using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EmoMe.Common.Enums;

namespace EmoMe.Services.Interfaces
{
    public interface IDialogService
    {
        Task ShowError(string title, string message, string buttonText, Action afterHideCallback);

        Task ShowError(string title, Exception error, string buttonText, Action afterHideCallback);

        Task ShowMessage(string title, string message);

        Task ShowMessage(string title, string message, string buttonText, Action afterHideCallback);

        Task<bool> ShowMessage(string title, string message, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback = null);

        Task<string> ShowActionSheet(string title, string cancel, string destruction, CancellationToken cancellationToken, Action<string> afterHideCallback, params string[] buttons);

        void ShowLoading(string title, MaskType maskType);

        void HideLoading();

        Task ShowMessage(string title, string message, Action afterHideCallback);

        void ShowToastMessage(string title, string message, ToastNotificationType toastNotificationType);
    }
}