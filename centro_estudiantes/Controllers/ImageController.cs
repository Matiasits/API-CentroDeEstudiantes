using centro_estudiantes.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using centro_estudiantes.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly DataContext dataContext;

    public ImageController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet("{idImagen}")]
    public IActionResult GetImage(long idImagen)
    {
        Image imageDB = this.dataContext.Image.Find(idImagen);
        if (imageDB == null)
        {
            return NotFound("Imagen no encontrada");
        }

        return File(imageDB.ImageData, "image/jpg");
    }

    [HttpPost]
    public IActionResult UploadImage([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Archivo no válido");
        }

        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);

            var image = new Image
            {
                ImageData = memoryStream.ToArray(),
                UploadDate = DateTime.Now
            };

            dataContext.Image.Add(image);
            dataContext.SaveChanges();

            return Ok(new { image.Id });
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"El tamaño del archivo no debe superar {_maxFileSize} bytes.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedExtensionsAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!_allowedExtensions.Contains(fileExtension))
                {
                    return new ValidationResult($"Solo se permiten archivos con las siguientes extensiones: {string.Join(", ", _allowedExtensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class FileInputModel
    {
        [Required(ErrorMessage = "El archivo es obligatorio.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)] // Ajusta el tamaño máximo permitido según tus necesidades
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" })] // Ajusta las extensiones permitidas según tus necesidades
        public IFormFile File { get; set; }
    }
}
