using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvioMaritimoController : ControllerBase
    {
        private readonly EnvioMaritimoContext _dbContext;

        public EnvioMaritimoController(EnvioMaritimoContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Obtener todos los envios maritimos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioMaritimo>>> GetEnvioMaritimo()
        {
            return await _dbContext.EnvioMaritimo.ToListAsync();
        }

        //Obtener un envio maritimo por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvioMaritimo>> GetEnvioMaritimo(int id)
        {
            if (!_dbContext.EnvioMaritimo.Any())
            {
                return NotFound("No hay envíos marítimos disponibles.");
            }

            var envioMaritimo = await _dbContext.EnvioMaritimo.FindAsync(id);
            if (envioMaritimo == null)
            {
                return NotFound($"Envío marítimo con ID {id} no encontrado.");
            }

            return envioMaritimo;
        }

        //Agregar un envio maritimo
        [HttpPost]
        public async Task<ActionResult<EnvioMaritimo>> PostEnvioMaritimo(EnvioMaritimo envioMaritimo)
        {
            try
            {
                // Validar que el Puerto y el Plan existan
                if (!_dbContext.Puerto.Any(p => p.PuertoID == envioMaritimo.PuertoEntregaID) ||
                    !_dbContext.PlanDeEntrega.Any(p => p.PlanID == envioMaritimo.PlanID))
                {
                    return BadRequest("El Puerto o el Plan especificados no existen.");
                }

                _dbContext.EnvioMaritimo.Add(envioMaritimo);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEnvioMaritimo), new { id = envioMaritimo.EnvioMaritimoID }, envioMaritimo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el envío marítimo.");
            }
        }
        //Actualizar un envio maritimo por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvioMaritimo(int id, EnvioMaritimo envioMaritimo)
        {
            if (id != envioMaritimo.EnvioMaritimoID)
            {
                return BadRequest("El ID del envío marítimo no coincide.");
            }

            _dbContext.Entry(envioMaritimo).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvioMaritimoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Borrar un envio maritimo por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvioMaritimo(int id)
        {
            var envioMaritimo = await _dbContext.EnvioMaritimo.FindAsync(id);
            if (envioMaritimo == null)
            {
                return NotFound($"Envío marítimo con ID {id} no encontrado.");
            }

            _dbContext.EnvioMaritimo.Remove(envioMaritimo);
            await _dbContext.SaveChangesAsync();

            return Ok($"El envío marítimo con ID {id} se eliminó correctamente.");
        }

        private bool EnvioMaritimoExists(int id)
        {
            return _dbContext.EnvioMaritimo.Any(e => e.EnvioMaritimoID == id);
        }
    }
}

