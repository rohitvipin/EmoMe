using SQLite;

namespace EmoMe.Services.Interfaces
{
    public interface ISqLiteService
    {
        SQLiteConnection GetConnection();
    }
}