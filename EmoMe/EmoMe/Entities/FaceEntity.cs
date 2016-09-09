using System;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face.Contract;

namespace EmoMe.Entities
{
    public class FaceEntity : BaseEntity
    {
        private FaceRectangle _faceRectangle;
        private FaceLandmarks _faceLandmarks;
        private FaceAttributes _faceAttributes;
        private Scores _emotions;
        public Guid FaceId { get; set; }

        public FaceRectangle FaceRectangle
        {
            get { return _faceRectangle; }
            set
            {
                _faceRectangle = value;
                OnPropertyChanged();
            }
        }

        public FaceLandmarks FaceLandmarks
        {
            get { return _faceLandmarks; }
            set
            {
                _faceLandmarks = value;
                OnPropertyChanged();
            }
        }

        public FaceAttributes FaceAttributes
        {
            get { return _faceAttributes; }
            set
            {
                _faceAttributes = value;
                OnPropertyChanged();
            }
        }

        public Scores Emotions
        {
            get { return _emotions; }
            set
            {
                _emotions = value;
                OnPropertyChanged();
            }
        }
    }
}