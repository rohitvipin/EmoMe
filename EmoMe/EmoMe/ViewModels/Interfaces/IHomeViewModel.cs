using System.Collections.ObjectModel;
using EmoMe.Common;
using EmoMe.Entities;

namespace EmoMe.ViewModels.Interfaces
{
    public interface IHomeViewModel : IViewModel
    {
        ObservableCollection<ImageEntity> ImageDetails { get; set; }

        AsyncRelayCommand TakePhotoCommand { get; }

        AsyncRelayCommand PickPhotoCommand { get; }

        AsyncRelayCommand FilterPhotoListCommand { get; }
    }
}