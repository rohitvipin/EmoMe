using System.Threading.Tasks;
using EmoMe.Entities;
using Microsoft.ProjectOxford.Emotion.Contract;
using Plugin.Media.Abstractions;

namespace EmoMe.ViewModels.Interfaces
{
    public interface IImageDetailViewModel : IViewModel
    {
        ImageDetailEntity ImageDetails { get; set; }

        Task Initialize(int imageId, MediaFile imageFile);
    }
}