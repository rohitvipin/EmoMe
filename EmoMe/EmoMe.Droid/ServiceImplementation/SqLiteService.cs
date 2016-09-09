using System;
using System.IO;
using EmoMe.Common;
using EmoMe.Droid.ServiceImplementation;
using EmoMe.Services.Interfaces;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteService))]
namespace EmoMe.Droid.ServiceImplementation
{
    public class SqLiteService : ISqLiteService
    {
        public SQLiteConnection GetConnection() => new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), Constants.DatabaseFileName));
    }
}