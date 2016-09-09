using System;
using System.Threading.Tasks;
using EmoMe.Common;
using EmoMe.Services.Interfaces;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media.Abstractions;
using Face = Microsoft.ProjectOxford.Face.Contract.Face;

namespace EmoMe.Services
{
    public class FaceService : IFaceService
    {
        /// <summary>
        /// Detects the faces from the given image
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        public async Task<Face[]> GetFacialFeature(MediaFile imageFile)
        {
            var requiredFaceAttributes = new[]
            {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Smile,
                FaceAttributeType.FacialHair,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Glasses
             };

            using (var imageStream = imageFile.GetStream())
            {
                using (var faceServiceClient = new FaceServiceClient(Constants.FaceServiceClientApiKey))
                {
                    return await faceServiceClient.DetectAsync(imageStream, returnFaceLandmarks: true, returnFaceAttributes: requiredFaceAttributes);                  
                }
            }
        }

        /// <summary>
        /// Detects the emototions from the given image
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        public async Task<Emotion[]> GetFaceEmotions(MediaFile imageFile)
        {
            using (var imageUrl = imageFile.GetStream())
            {
                var faceEmotions = new EmotionServiceClient(Constants.EmotionServiceClientApiKey);
                //Detect emotions from photo
                return await faceEmotions.RecognizeAsync(imageUrl, null);
            }
        }

        /// <summary>
        /// Detects
        /// </summary>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        public async Task<AnalysisResult> GetImageVisionDetails(MediaFile imageFile)
        {
            var visualFeatures = new[]
                           {
                    VisualFeature.Adult,
                    VisualFeature.Categories,
                    VisualFeature.Color,
                    VisualFeature.Description,
                    VisualFeature.Faces,
                    VisualFeature.ImageType,
                    VisualFeature.Tags
                };

            using (var imageUrl = imageFile.GetStream())
            {
                var imageVision = new VisionServiceClient(Constants.ComputerVisionClientApiKey);
                //Detect image Visions details
                return await imageVision.AnalyzeImageAsync(imageUrl, visualFeatures);
            }
        }
    }
}

