using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioTerrestreController : ControllerBase
    {
        private readonly EnvioTerrestreContext _dbContext;

        public EnvioTerrestreController(EnvioTerrestreContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los envíos terrestres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioTerrestre>>> GetEnviosTerrestres()
        {
            return await _dbContext.EnvioTerrestre.ToListAsync();
        }

        // Obtener un envío terrestre por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvioTerrestre>> GetEnvioTerrestre(int id)
        {
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
                _dbContext.EnvioTerrestre.Add(envioTerrestre);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEnvioTerrestre), new { id = envioTerrestre.EnvioTerrestreID }, envioTerrestre);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el envío terrestre: " + ex.Message);
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
