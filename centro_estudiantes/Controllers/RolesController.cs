using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using centro_estudiantes.Data;
using centro_estudiantes.dto.Roles;
using centro_estudiantes.Entities.Rol;

namespace centro_estudiantes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolController : ControllerBase
    {
        private readonly ILogger<RolController> _logger;
        private readonly DataContext _dataContext;

        public RolController(ILogger<RolController> logger, DataContext dataContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        [HttpGet(Name = "GetRoles")]
        public async Task<ActionResult<List<RolDTO>>> Get()
        {
            try
            {
                // Obtener roles
                List<RolDTO> listaRoles = await _dataContext.Rol
                    .Select(rol => new RolDTO
                    {
                        IdRol = rol.IdRol,
                        NombreRol = rol.NombreRol
                    })
                    .ToListAsync();

                return Ok(listaRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de Roles.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}", Name = "GetRolById")]
        public async Task<IActionResult> ObtenerRolPorId(int id)
        {
            try
            {
                // Obtener el usuario desde la base de datos por su ID
                var rol = await _dataContext.Rol.FindAsync(id);

                // Verificar si el usuario existe
                if (rol == null)
                {
                    return NotFound($"No se encontró un usuario con ID: {id}");
                }

                // Mapear el usuario a un DTO si es necesario
                var rolDTO = new
                {
                    rol.IdRol,
                    rol.NombreRol,
                };

                // Retornar el usuario en formato JSON
                return Ok(rolDTO);
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarRol(int id, [FromBody] RolInputModel RolInput)
        {
            try
            {
                // Validar el modelo antes de intentar la actualización
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Obtener el usuario desde la base de datos por su ID
                var rol = await _dataContext.Rol.FindAsync(id);

                // Verificar si el usuario existe
                if (rol == null)
                {
                    return NotFound($"No se encontró un usuario con ID: {id}");
                }

                // Actualizar propiedades del usuario con los datos proporcionados
                rol.IdRol = RolInput.IdRol;
                rol.NombreRol = RolInput.NombreRol;

                // Guardar los cambios en la base de datos
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "Usuario actualizado exitosamente", rolId = rol.IdRol});
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol([FromBody] RolInputModel rolInput)
        {
            try
            {
                // Validar el modelo antes de agregarlo a la base de datos
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapear el modelo de entrada al modelo de base de datos
                var rolDTO = new RolDTO
                {
                    IdRol = rolInput.IdRol,
                    NombreRol = rolInput.NombreRol,
                };

                // Convertir DTO a entidad
                var rol = new Rol
                {
                    IdRol = rolInput.IdRol,
                    NombreRol = rolInput.NombreRol,
                };

                // Agregar a la base de datos
                _dataContext.Rol.Add(rol);
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "Usuario creado exitosamente", RolId = rol.IdRol});
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}", Name = "DeleteRol")]
        public async Task<IActionResult> EliminarRol(int id)
        {
            try
            {
                var rol = await _dataContext.Rol.FindAsync(id);

                if (rol == null)
                {
                    return NotFound($"No se encontró un rol con ID: {id}");
                }

                // Realiza la eliminación del rol
                _dataContext.Rol.Remove(rol);
                await _dataContext.SaveChangesAsync();

                return Ok(new { Mensaje = "Rol eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        public class RolInputModel
        {
            public int IdRol { get; set; }
            public string NombreRol { get; set; } = null!;
        }
    }
}
