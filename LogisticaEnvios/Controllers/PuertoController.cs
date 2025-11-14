using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuertoController : ControllerBase
    {
        private readonly PuertoContext _dbContext;

        public PuertoController(PuertoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puerto>>> GetPuertos()
        {
            return await _dbContext.Puerto.ToListAsync();
        }

        [HttpGet("{puertoID}")]
        public async Task<ActionResult<Puerto>> GetPuerto(int puertoID)
        {
            if (!_dbContext.Puerto.Any())
            {
                return NotFound("No hay puertos disponibles.");
            }

            var puerto = await _dbContext.Puerto.FindAsync(puertoID);
            if (puerto == null)
            {
                return NotFound($"Puerto con ID {puertoID} no encontrado.");
            }

            return puerto;
        }

        [HttpPost]
        public async Task<ActionResult<Puerto>> PostPuerto(Puerto puerto)
        {
            try
            {
                if (_dbContext.Puerto.Any(p => p.Nombre == puerto.Nombre))
                {
                    return BadRequest($"Ya existe un puerto con el nombre {puerto.Nombre}.");
                }

                _dbContext.Puerto.Add(puerto);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPuerto), new { PuertoID = puerto.PuertoID }, puerto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el puerto.");
            }
        }

        [HttpPut("{puertoID}")]
        public async Task<IActionResult> PutPuerto(int puertoID, Puerto puerto)
        {
            if (puertoID != puerto.PuertoID)
            {
                return BadRequest("El ID del puerto no coincide.");
            }

            _dbContext.Entry(puerto).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuertoExists(puertoID))
                {
                    return NotFound($"Puerto con ID {puertoID} no encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Se actualizó el puerto con ID: {puertoID}");
        }

        private bool PuertoExists(int puertoID)
        {
            return _dbContext.Puerto.Any(p => p.PuertoID == puertoID);
        }

        [HttpDelete("{puertoID}")]
        public async Task<IActionResult> DeletePuerto(int puertoID)
        {
            if (!_dbContext.Puerto.Any())
            {
                return NotFound("No hay puertos registrados.");
            }

            var puerto = await _dbContext.Puerto.FindAsync(puertoID);
            if (puerto == null)
            {
                return NotFound($"El puerto con ID {puertoID} no existe.");
            }

            _dbContext.Puerto.Remove(puerto);
            await _dbContext.SaveChangesAsync();

            return Ok($"El puerto con ID {puertoID} se eliminó correctamente.");
        }
    }
}
