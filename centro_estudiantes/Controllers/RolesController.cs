using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using centro_estudiantes.Data;

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
        public async Task<ActionResult<List<string>>> Get()
        {
            try
            {
                // Obtener roles
                List<string> listaRoles = await _dataContext.Rol
                    .Select(rol => rol.NombreRol)
                    .ToListAsync();

                return Ok(listaRoles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de Roles.");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
