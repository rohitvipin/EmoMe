using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EmoMe.Models;
using EmoMe.Repositories.Interfaces;
using EmoMe.Services.Interfaces;
using Xamarin.Forms;

namespace EmoMe.Repositories
{
    public class ImageDetailRepository : IImageDetailRepository
    {

        public ImageDetailRepository()
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                //Create table if not exist
                sqlconnection.CreateTable<ImageModel>();
            }
        }

        /// <summary>
        /// Get a list of all the items in the Table
        /// </summary>
        /// <returns></returns>
        public IList<ImageModel> GetAll()
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                return sqlconnection.Table<ImageModel>().ToList();
            }
        }

        /// <summary>
        /// Get a list of items in the table filtered by the condition
        /// </summary>
        /// <returns></returns>
        public IList<ImageModel> Get(Expression<Func<ImageModel, bool>> whereExpression)
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                return sqlconnection.Table<ImageModel>().Where(whereExpression).ToList();
            }
        }

        /// <summary>
        /// Get the item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ImageModel GetById(int id)
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                return sqlconnection.Table<ImageModel>().FirstOrDefault(t => t.Id == id);
            }
        }

        /// <summary>
        /// Delete object by Id
        /// </summary>
        /// <param name="id"></param>
        public int Delete(int id)
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                return sqlconnection.Delete<ImageModel>(id);
            }
        }

        /// <summary>
        /// Insert new item to table and return the Primary Key
        /// </summary>
        /// <param name="obj"></param>
        public int Insert(ImageModel obj)
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                sqlconnection.Insert(obj);
                return obj.Id;
            }
        }

        /// <summary>
        /// Update an object in the database
        /// </summary>
        /// <param name="obj"></param>
        public void Update(ImageModel obj)
        {
            using (var sqlconnection = DependencyService.Get<ISqLiteService>().GetConnection())
            {
                sqlconnection.Update(obj);
            }
        }
    }
}