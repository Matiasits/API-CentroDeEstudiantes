namespace centro_estudiantes.Entities
{
    public class Image
    {
        public long Id { get; set; } = 0;

        public byte[] ImageData { get; set; } = null;

        public DateTime? UploadDate { get; set; } = null;
    }
}