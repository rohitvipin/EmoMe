using System.Collections.ObjectModel;
using EmoMe.Common;
using EmoMe.Entities;

namespace EmoMe.ViewModels.Interfaces
{
    public interface IImageFilterViewModel : IViewModel
    {
        ObservableCollection<ImageFilterEnity> ImageFilterEnities { get; set; }

        AsyncRelayCommand DoneCommand { get; }
    }
}