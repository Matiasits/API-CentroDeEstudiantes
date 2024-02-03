// Asegúrate de agregar los using necesarios al principio del archivo
using Microsoft.AspNetCore.Mvc;
using centro_estudiantes.Data; // o el namespace donde estén tus modelos
using centro_estudiantes.Entities.Choripanes;
using centro_estudiantes.dto.Choripan;

namespace centro_estudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoriPanController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ChoriPanController(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        [HttpPost]
        public async Task<IActionResult> CrearChoriPan([FromBody] ChoriPanInputModel choriPanInput)
        {
            try
            {
                // Validar el modelo antes de agregarlo a la base de datos
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapear el modelo de entrada al modelo de base de datos
                var choripanDTO = new ChoripanDTO
                {
                    IdTipoChori = choriPanInput.TipoChoriId,
                    Verdura = choriPanInput.Verdura,
                    Pan = choriPanInput.Pan,
                    Aderezo = choriPanInput.Aderezo,
                    IdUsuario = choriPanInput.UsuarioId,
                    FHAlta = DateTime.Now,
                    FHBaja = null // La FHBaja se inicializa como null al crear el registro
                };

                // Crear una nueva entidad Choripan y asignar las propiedades del DTO
                var choripanEntity = new Choripan
                {
                    IdTipoChori = choripanDTO.IdTipoChori,
                    Verdura = choripanDTO.Verdura,
                    Pan = choripanDTO.Pan,
                    Aderezo = choripanDTO.Aderezo,
                    IdUsuario = choripanDTO.IdUsuario,
                    FHAlta = choripanDTO.FHAlta,
                    FHBaja = choripanDTO.FHBaja
                };

                // Agregar a la base de datos
                _dataContext.Choripan.Add(choripanEntity);
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "ChoriPan creado exitosamente", ChoriPanId = choripanEntity.IdChoripan });
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    // Modelo de entrada para el método CrearChoriPan
    public class ChoriPanInputModel
    {
        public int TipoChoriId { get; set; }
        public string? Verdura { get; set; }
        public string? Pan { get; set; }
        public string? Aderezo { get; set; }
        public int UsuarioId { get; set; }
    }
}