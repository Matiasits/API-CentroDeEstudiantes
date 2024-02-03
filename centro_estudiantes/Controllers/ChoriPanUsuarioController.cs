using Microsoft.AspNetCore.Mvc;
using centro_estudiantes.Data;
using centro_estudiantes.Entities.Usuarios;
using centro_estudiantes.Entities.Choripanes;
using centro_estudiantes.dto.Usuarios;
using centro_estudiantes.dto.Choripan;
using System;
using System.Threading.Tasks;

namespace centro_estudiantes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoriPanUsuarioController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ChoriPanUsuarioController(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuarioYChoriPan([FromBody] CrearUsuarioYChoriPanInputModel input)
        {
            try
            {
                // Validar el modelo antes de agregarlo a la base de datos
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Crear Usuario
                var usuarioDTO = new UsuarioDTO
                {
                    NombreCompleto = input.NombreCompleto,
                    CorreoElectronico = input.CorreoElectronico,
                    Telefono = input.Telefono,
                    IdRol = input.IdRol
                };

                var usuarioEntity = new Usuario
                {
                    NombreCompleto = usuarioDTO.NombreCompleto,
                    CorreoElectronico = usuarioDTO.CorreoElectronico,
                    Telefono = usuarioDTO.Telefono,
                    IdRol = usuarioDTO.IdRol,
                    FHAlta = DateTime.Now,
                    FHBaja = null
                };

                _dataContext.Usuario.Add(usuarioEntity);
                await _dataContext.SaveChangesAsync();

                // Crear Choripan
                var choripanDTO = new ChoripanDTO
                {
                    IdTipoChori = input.TipoChoriId,
                    Verdura = input.Verdura,
                    Pan = input.Pan,
                    Aderezo = input.Aderezo,
                    IdUsuario = usuarioEntity.IdUsuario,
                    FHAlta = DateTime.Now,
                    FHBaja = null
                };

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

                _dataContext.Choripan.Add(choripanEntity);
                await _dataContext.SaveChangesAsync();

                // Retornar el resultado exitoso
                return Ok(new { Mensaje = "Usuario y ChoriPan creados exitosamente", UsuarioId = usuarioEntity.IdUsuario, ChoriPanId = choripanEntity.IdChoripan });
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar una respuesta adecuada
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

    // Modelo de entrada para el método CrearUsuarioYChoriPan
    public class CrearUsuarioYChoriPanInputModel
    {
        public int TipoChoriId { get; set; }
        public string? Verdura { get; set; }
        public string? Pan { get; set; }
        public string? Aderezo { get; set; }

        // Propiedades para la creación de Usuario
        public string NombreCompleto { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int IdRol { get; set; }
    }
}
