using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Common.Enums;
using EmoMe.Entities;
using EmoMe.Services.Interfaces;
using EmoMe.ViewModels.Interfaces;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using EmoMe.Common.Helpers;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace EmoMe.ViewModels
{
    public class ImageDetailViewModel : BaseViewModel, IImageDetailViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ILoggingService _loggingService;
        private readonly IDataAccessService _dataAccessService;
        private readonly IFaceService _faceService;

        private ImageDetailEntity _imageDetails;

        public ImageDetailViewModel(IDialogService dialogService, ILoggingService loggingService, IDataAccessService dataAccessService, IFaceService faceService)
            : base(dialogService, loggingService)
        {
            _dialogService = dialogService;
            _loggingService = loggingService;
            _dataAccessService = dataAccessService;
            _faceService = faceService;
        }

        public override string PageTitle => PageTitles.ImageDetailTitle;

        public ImageDetailEntity ImageDetails
        {
            get { return _imageDetails; }
            set
            {
                _imageDetails = value;
                OnPropertyChanged();
            }
        }
        public override Task Initialize()
        {
            throw new NotImplementedException();
        }

        public async Task Initialize(int imageId, MediaFile imageFile)
        {
            try
            {
                BeginBusy();

                ImageDetails = null;

                var image = _dataAccessService.GetImageById(imageId);
                
                ImageDetails = new ImageDetailEntity
                {
                    Image = new ImageEntity(null, null, null)
                    {
                        Path = image.Path,
                        //Description = imageVisionDetails.Result.Description.ToString(),
                        ImageId = image.Id,
                        //Title = imageVisionDetails.Result.ImageType.ToString()
                    }
                };

                if (imageFile != null)
                {
                    //When a new image is picked from the gallery or camera
                    var imageVisionDetails = await _faceService.GetImageVisionDetails(imageFile);

                    var faceEmotionsTask = _faceService.GetFaceEmotions(imageFile);

                    var facesArray = await _faceService.GetFacialFeature(imageFile);

                    ImageDetails.Faces = facesArray.Select(x => new FaceEntity
                    {
                        FaceAttributes = x.FaceAttributes,
                        FaceId = x.FaceId,
                        FaceLandmarks = x.FaceLandmarks,
                        FaceRectangle = x.FaceRectangle
                    })
                    .ToList();

                    var emotionsArray = await faceEmotionsTask;

                    for (var index = 0; index < ImageDetails.Faces.Count; index++)
                    {
                        //get Percentage of the emotions
                        var emotions = Helper.DisplayPercentage(emotionsArray[index]?.Scores);
                        var faceAttribute = Helper.DisplayPercentage(facesArray[index].FaceAttributes);

                        ImageDetails.Faces[index].FaceAttributes = faceAttribute;                      
                        ImageDetails.Faces[index].Emotions = emotions;
                    }

                    //Update database with values fetched from API
                    var title = string.Join(", ", imageVisionDetails.Description.Captions.Select(x=>x.Text));
                    image.Title = title.Replace("_", "");

                    var description = string.Join(", ", imageVisionDetails.Categories.Select(x => x.Name));
                    image.Description = description.Replace("_","");

                    image.Faces = JsonConvert.SerializeObject(ImageDetails.Faces);
                    _dataAccessService.UpdateImageDetails(image);
                }
                else
                {
                    //Already the details of the image are present in the database
                    if (string.IsNullOrWhiteSpace(image.Faces) == false)
                    {
                        ImageDetails.Faces = JsonConvert.DeserializeObject<IList<FaceEntity>>(image.Faces);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                _dialogService.ShowToastMessage(PageTitles.ImageDetailTitle, Messages.UnExpectedError, ToastNotificationType.Error);
            }
            finally
            {
                EndBusy();
            }
        }
    }
}
