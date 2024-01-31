using Microsoft.AspNetCore.Mvc;
using centro_estudiantes.Data;
using centro_estudiantes.Entities.Usuarios;
using centro_estudiantes.dto.Usuarios;

namespace centro_estudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UsuarioController(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioInputModel usuarioInput)
        {
            try
            {
                // Validar el modelo antes de agregarlo a la base de datos
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapear el modelo de entrada al modelo de base de datos
                var usuarioDTO = new UsuarioDTO
                {
                    NombreCompleto = usuarioInput.NombreCompleto,
                    CorreoElectronico = usuarioInput.CorreoElectronico,
                    Telefono = usuarioInput.Telefono,
                    IdRol = usuarioInput.IdRol,
                };

                // Convertir DTO a entidad
                var usuario = new Usuario
                {
                    NombreCompleto = usuarioDTO.NombreCompleto,
                    CorreoElectronico = usuarioDTO.CorreoElectronico,
                    Telefono = usuarioDTO.Telefono,
                    IdRol = usuarioDTO.IdRol,
                    FHAlta = DateTime.Now,
                    FHBaja = null // La FHBaja se inicializa como null al crear el registro
                };

                // Agregar a la base de datos
                _dataContext.Usuario.Add(usuario);
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "Usuario creado exitosamente", UsuarioId = usuario.IdUsuario });
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    // Modelo de entrada para el método CrearUsuario
    public class UsuarioInputModel
    {
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public int IdRol {get; set;}
    }
}
