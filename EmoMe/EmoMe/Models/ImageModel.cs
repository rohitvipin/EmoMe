using SQLite;

namespace EmoMe.Models
{
    public class ImageModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        //send serialized data into the model
        public string Faces { get; set; }
    }
}