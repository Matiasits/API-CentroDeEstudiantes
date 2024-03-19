using System;
using Microsoft.AspNetCore.Mvc;
using centro_estudiantes.Entities;
using centro_estudiantes.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; 

namespace centro_estudiantes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly List<Image> _images = new List<Image>(); // Lista para simular almacenamiento de imágenes
        private readonly ILogger<ImageController> _logger; // Agrega esta variable de clase

        public ImageController(ILogger<ImageController> logger) // Agrega este constructor para inyectar ILogger
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<long> UploadImage([FromForm] ImageDTO imageDTO)
        {
            try
            {
                // Verificar si se envió algún archivo
                if (imageDTO.ImageData == null || imageDTO.ImageData.Length == 0)
                {
                    return BadRequest("No se proporcionó ningún archivo de imagen.");
                }

                // Leer los datos de la imagen desde el archivo
                byte[] imageData;
                using (var memoryStream = new MemoryStream())
                {
                    imageDTO.ImageData.CopyTo(memoryStream);
                    imageData = memoryStream.ToArray();
                }

                // Crear una instancia de Image
                var image = new Image
                {
                    Id = imageDTO.Id,
                    ImageData = imageData,
                    UploadDate = imageDTO.UploadDate ?? DateTime.UtcNow // Si no se especifica la fecha, usar la fecha actual
                };

                // Simular el almacenamiento de la imagen (podrías guardarla en una base de datos o en un almacenamiento en la nube)
                _images.Add(image);

                return Ok(image.Id); // Devolver el ID de la imagen cargada
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la imagen.");
                return StatusCode(500, "Error interno del servidor al cargar la imagen.");
            }
        }


        [HttpGet("{id}")]
        public ActionResult GetImage(long id)
        {
            try
            {
                // Buscar la imagen por su ID
                var image = _images.Find(i => i.Id == id);

                if (image == null)
                    return NotFound(); // Si no se encuentra la imagen, devolver 404 Not Found

                // Devolver la imagen como un archivo de contenido binario
                return File(image.ImageData, "image/jpeg"); // Cambia "image/jpeg" al tipo de contenido adecuado según el formato de tus imágenes
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la imagen.");
                return StatusCode(500, "Error interno del servidor al obtener la imagen.");
            }
        }
    }
}
