using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    public class EnvioTerrestreController : ControllerBase
    {
        private readonly EnvioMaritimoContext _dbContext;

        public EnvioTerrestreController(EnvioMaritimoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/[controller]")]
        [ApiController]
        public class EnviosTerrestresController : ControllerBase
        {
            private readonly EnvioTerrestreContext _dbContext;

            public EnviosTerrestresController(EnvioTerrestreContext dbContext)
            {
                _dbContext = dbContext;
            }

            // Obtener todos los envíos terrestres
            [HttpGet]
            public async Task<ActionResult<IEnumerable<EnvioTerrestre>>> GetEnviosTerrestres()
            {
                if (!_dbContext.EnvioTerrestre.Any())
                {
                    return NotFound("No hay envíos terrestres disponibles.");
                }

                return await _dbContext.EnvioTerrestre.ToListAsync();
            }

            // Obtener un envío terrestre por ID
            [HttpGet("{id}")]
            public async Task<ActionResult<EnvioTerrestre>> GetEnvioTerrestre(int id)
            {
                if (!_dbContext.EnvioTerrestre.Any())
                {
                    return NotFound("No hay envíos terrestres disponibles.");
                }

                var envioTerrestre = await _dbContext.EnvioTerrestre.FindAsync(id);

                if (envioTerrestre == null)
                {
                    return NotFound($"Envío terrestre con ID {id} no encontrado.");
                }

                return envioTerrestre;
            }

            // Crear un nuevo envío terrestre
            [HttpPost]
            public async Task<ActionResult<EnvioTerrestre>> PostEnvioTerrestre(EnvioTerrestre envioTerrestre)
            {
                try
                {
                    // Validar si la bodega y el plan de entrega existen
                    if (!_dbContext.Bodega.Any(b => b.BodegaID == envioTerrestre.BodegaEntregaID) ||
                        !_dbContext.PlanDeEntrega.Any(p => p.PlanID == envioTerrestre.PlanID))
                    {
                        return BadRequest("La bodega de entrega o el plan de entrega no existen.");
                    }

                    _dbContext.EnvioTerrestre.Add(envioTerrestre);
                    await _dbContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetEnvioTerrestre), new { id = envioTerrestre.EnvioTerrestreID }, envioTerrestre);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el envío terrestre.");
                }
            }

            // Actualizar un envío terrestre por ID
            [HttpPut("{id}")]
            public async Task<IActionResult> PutEnvioTerrestre(int id, EnvioTerrestre envioTerrestre)
            {
                if (id != envioTerrestre.EnvioTerrestreID)
                {
                    return BadRequest("La ID del envío terrestre no coincide.");
                }

                // Validar si la bodega y el plan de entrega existen
                if (!_dbContext.Bodega.Any(b => b.BodegaID == envioTerrestre.BodegaEntregaID) ||
                    !_dbContext.PlanDeEntrega.Any(p => p.PlanID == envioTerrestre.PlanID))
                {
                    return BadRequest("La bodega de entrega o el plan de entrega no existen.");
                }

                _dbContext.Entry(envioTerrestre).State = EntityState.Modified;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvioTerrestreExists(id))
                    {
                        return NotFound("El envío terrestre no existe.");
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok("Se actualizó el envío terrestre con ID: " + id);
            }

            // Eliminar un envío terrestre por ID
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteEnvioTerrestre(int id)
            {
                if (!_dbContext.EnvioTerrestre.Any())
                {
                    return NotFound("No hay envíos terrestres registrados.");
                }

                var envioTerrestre = await _dbContext.EnvioTerrestre.FindAsync(id);
                if (envioTerrestre == null)
                {
                    return NotFound($"El envío terrestre con ID {id} no existe.");
                }

                _dbContext.EnvioTerrestre.Remove(envioTerrestre);
                await _dbContext.SaveChangesAsync();

                return Ok($"El envío terrestre con ID {id} se eliminó correctamente.");
            }

            private bool EnvioTerrestreExists(int id)
            {
                return _dbContext.EnvioTerrestre.Any(e => e.EnvioTerrestreID == id);
            }
        }
    }
}

