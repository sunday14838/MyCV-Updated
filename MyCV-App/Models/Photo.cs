namespace MyCV_App.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public required string ImagePath { get; set; }

        public string? Title { get; set; }
    }
}
