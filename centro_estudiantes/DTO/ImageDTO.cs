namespace centro_estudiantes.DTO
{
    public class ImageDTO
    {
        public long Id { get; set; } = 0;

        public byte[] ImageData { get; set; } = null;

        public DateTime? UploadDate { get; set; } = null;
        
    }
}
