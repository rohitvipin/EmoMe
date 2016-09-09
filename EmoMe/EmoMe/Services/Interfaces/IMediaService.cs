using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace EmoMe.Services.Interfaces
{
    public interface IMediaService
    {
        bool IsPickPhotoSupported { get; }
        bool IsCameraAvailable { get; }
        bool IsTakePhotoSupported { get; }
        Task<MediaFile> PickPhotoAsync();
        Task<MediaFile> TakePhotoAsync();
    }
}