using System.Collections.Generic;
using EmoMe.Entities;
using EmoMe.Models;

namespace EmoMe.Services.Interfaces
{
    public interface IDataAccessService
    {
        int InsertImageDetails(ImageModel imageModel);

        IEnumerable<ImageModel> GetImageDetails();

        ImageModel GetImageById(int imageId);

        void UpdateImageDetails(ImageModel image);
    }
}