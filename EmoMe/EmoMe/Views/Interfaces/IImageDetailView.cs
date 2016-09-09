using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;
using Plugin.Media.Abstractions;

namespace EmoMe.Views.Interfaces
{
    public interface IImageDetailView : IView
    {
        Task Initialize(int imageId, MediaFile imageFile);
    }
}