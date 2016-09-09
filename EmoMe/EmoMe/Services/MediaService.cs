using System;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Services.Interfaces;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace EmoMe.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMedia _current = CrossMedia.Current;

        public bool IsPickPhotoSupported => _current.IsPickPhotoSupported;

        public bool IsCameraAvailable => _current.IsCameraAvailable;

        public bool IsTakePhotoSupported => _current.IsTakePhotoSupported;

        public async Task<MediaFile> PickPhotoAsync() => await _current.PickPhotoAsync();

        public async Task<MediaFile> TakePhotoAsync()
        {
            // Supply media options for saving our photo after it's taken (internal storage)
            return await _current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = Constants.DefaultDirectory,
                Name = $"{PageTitles.ApplicationTitle}_{DateTime.Now.Ticks}"
            });
        }
    }
}