namespace centro_estudiantes.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }

        public IFormFile ImageData { get; set; } = null!;

        public DateTime? UploadDate { get; set; } = null;
        
    }
}
