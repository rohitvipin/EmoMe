using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmoMe.Models;

namespace EmoMe.Repositories.Interfaces
{
    public interface IImageDetailRepository
    {
        IList<ImageModel> GetAll();
        IList<ImageModel> Get(Expression<Func<ImageModel, bool>> whereExpression);
        ImageModel GetById(int id);
        int Delete(int id);
        int Insert(ImageModel obj);
        void Update(ImageModel obj);
    }
}