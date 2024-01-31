using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using centro_estudiantes.Data;

namespace centro_estudiantes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoChoriController : ControllerBase
    {
        private readonly ILogger<TipoChoriController> _logger;
        private readonly DataContext _dataContext;

        public TipoChoriController(ILogger<TipoChoriController> logger, DataContext dataContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        [HttpGet(Name = "GetTiposChori")]
        public async Task<ActionResult<List<string>>> Get()
        {
            try
            {
                // Obtener tipos de choris
                List<string> listaTipoChori = await _dataContext.TipoChori
                    .Select(tipoChori => tipoChori.TipoDeChori)
                    .Distinct()
                    .ToListAsync();

                return Ok(listaTipoChori);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de TipoChori.");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
