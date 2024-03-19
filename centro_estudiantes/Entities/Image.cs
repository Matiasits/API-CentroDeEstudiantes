namespace centro_estudiantes.Entities
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; } = null!;

        public DateTime? UploadDate { get; set; } = null;
    }
}