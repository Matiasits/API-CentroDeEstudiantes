using Microsoft.AspNetCore.Mvc;
using centro_estudiantes.Data;
using centro_estudiantes.Entities.Usuarios;
using centro_estudiantes.dto.Usuarios;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet(Name = "GetUsuario")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            try
            {
                // Obtener la lista de usuarios desde la base de datos
                var usuarios = await _dataContext.Usuario.ToListAsync();

                // Mapear la lista de entidades a una lista de DTOs si es necesario
                var usuariosDTO = usuarios.Select(u => new
                {
                    u.IdUsuario,
                    u.NombreCompleto,
                    u.CorreoElectronico,
                    u.Telefono,
                    u.IdRol
                }).ToList();

                // Retornar la lista de usuarios en formato JSON
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }




        [HttpGet("{id}", Name = "GetUsuarioById")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            try
            {
                // Obtener el usuario desde la base de datos por su ID
                var usuario = await _dataContext.Usuario.FindAsync(id);

                // Verificar si el usuario existe
                if (usuario == null)
                {
                    return NotFound($"No se encontró un usuario con ID: {id}");
                }

                // Mapear el usuario a un DTO si es necesario
                var usuarioDTO = new
                {
                    usuario.IdUsuario,
                    usuario.NombreCompleto,
                    usuario.CorreoElectronico,
                    usuario.Telefono,
                    usuario.IdRol
                };

                // Retornar el usuario en formato JSON
                return Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioInputModel usuarioInput)
        {
            try
            {
                // Validar el modelo antes de intentar la actualización
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Obtener el usuario desde la base de datos por su ID
                var usuario = await _dataContext.Usuario.FindAsync(id);

                // Verificar si el usuario existe
                if (usuario == null)
                {
                    return NotFound($"No se encontró un usuario con ID: {id}");
                }

                // Actualizar propiedades del usuario con los datos proporcionados
                usuario.NombreCompleto = usuarioInput.NombreCompleto;
                usuario.CorreoElectronico = usuarioInput.CorreoElectronico;
                usuario.Telefono = usuarioInput.Telefono;
                usuario.IdRol = usuarioInput.IdRol;

                // Guardar los cambios en la base de datos
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "Usuario actualizado exitosamente", UsuarioId = usuario.IdUsuario });
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
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
                    FHBaja = null
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

        [HttpDelete("{id}", Name = "DeleteUsuario")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var usuario = await _dataContext.Usuario.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound($"No se encontró un usuario con ID: {id}");
                }

                // Buscar y eliminar registros en 'choripan' que dependen de este usuario
                var registrosChoripan = await _dataContext.Choripan.Where(c => c.IdUsuario == id).ToListAsync();
                _dataContext.Choripan.RemoveRange(registrosChoripan);

                // Realiza la eliminación del usuario
                _dataContext.Usuario.Remove(usuario);
                await _dataContext.SaveChangesAsync();

                return Ok(new { Mensaje = "Usuario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }

    public class UsuarioInputModel
    {
        public string NombreCompleto { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int IdRol {get; set;}
    }
}
