using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Media.Abstractions;
using Face = Microsoft.ProjectOxford.Face.Contract.Face;

namespace EmoMe.Services.Interfaces
{
    public interface IFaceService
    {
        Task<Face[]> GetFacialFeature(MediaFile imageFile);

        Task<Emotion[]> GetFaceEmotions(MediaFile imageFile);

        Task<AnalysisResult> GetImageVisionDetails(MediaFile imageFile);
    }
}