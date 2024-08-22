namespace BusinessLayer.DTOs
{
    public class ImageUpload
    {
        public string Image { get; set; } = null!;
        public Guid UserId { get; set; }
        public string Type { get; set; } 
    }

}
