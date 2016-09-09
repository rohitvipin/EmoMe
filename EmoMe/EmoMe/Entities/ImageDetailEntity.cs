using System.Collections.Generic;

namespace EmoMe.Entities
{
    public class ImageDetailEntity : BaseEntity
    {
        private ImageEntity _image;
        private IList<FaceEntity> _faces;

        public ImageEntity Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public IList<FaceEntity> Faces
        {
            get { return _faces; }
            set
            {
                _faces = value;
                OnPropertyChanged();
            }
        }
    }
}