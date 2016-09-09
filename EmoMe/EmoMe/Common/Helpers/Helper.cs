using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face.Contract;
using static System.String;

namespace EmoMe.Common.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// Get perscentage of the emotion scores
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public static Scores DisplayPercentage(Scores scores)
        {
            var scoreInPercentage = new Scores();
            if (scores == null) return null;
            scoreInPercentage.Anger = float.Parse(scores.Anger.ToString("0.##")) * 100;
            scoreInPercentage.Contempt = float.Parse(scores.Contempt.ToString("0.##")) * 100;
            scoreInPercentage.Disgust = float.Parse(scores.Disgust.ToString("0.##")) * 100;
            scoreInPercentage.Fear = float.Parse(scores.Fear.ToString("0.##")) * 100;
            scoreInPercentage.Happiness = float.Parse(scores.Happiness.ToString("0.##")) * 100;
            scoreInPercentage.Neutral = float.Parse(scores.Neutral.ToString("0.##")) * 100;
            scoreInPercentage.Sadness = float.Parse(scores.Sadness.ToString("0.##")) * 100;
            scoreInPercentage.Surprise = float.Parse(scores.Surprise.ToString("0.##")) * 100;
            return scoreInPercentage;
        }

        public static FaceAttributes DisplayPercentage(double smile)
        {
            var facceAttributes = new FaceAttributes {Smile = smile * 100};
            return facceAttributes;
        }

        public static FaceAttributes DisplayPercentage(FaceAttributes faceAttributes)
        {
            var facceAttributes = new FaceAttributes
            {
                Age = faceAttributes.Age,
                Smile = faceAttributes.Smile * 100,
                FacialHair = faceAttributes.FacialHair,
                Gender = faceAttributes.Gender,
                Glasses = faceAttributes.Glasses,
                HeadPose = faceAttributes.HeadPose
            };
            return facceAttributes;
        }
    }
}
