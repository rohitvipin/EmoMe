using System.Collections.Generic;
using EmoMe.Entities;
using EmoMe.Models;
using EmoMe.Repositories.Interfaces;
using EmoMe.Services.Interfaces;

namespace EmoMe.Services
{
    public class DataAccessService : IDataAccessService
    {
        private readonly IImageDetailRepository _imageDetailRepository;

        public DataAccessService(IImageDetailRepository imageDetailRepository)
        {
            _imageDetailRepository = imageDetailRepository;
        }

        public int InsertImageDetails(ImageModel imageModel) => _imageDetailRepository.Insert(imageModel);

        public IEnumerable<ImageModel> GetImageDetails() => _imageDetailRepository.GetAll();

        public ImageModel GetImageById(int imageId) => _imageDetailRepository.GetById(imageId);

        public void UpdateImageDetails(ImageModel image) => _imageDetailRepository.Update(image);
    }
}