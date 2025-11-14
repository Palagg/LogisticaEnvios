using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly BodegaContext _dbContext;

        public BodegaController(BodegaContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todas las bodegas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
        {
            return await _dbContext.Bodega.ToListAsync();
        }

        // Obtener una bodega por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            if (!_dbContext.Bodega.Any())
            {
                return NotFound("No hay bodegas disponibles.");
            }

            var bodega = await _dbContext.Bodega.FindAsync(id);
            if (bodega == null)
            {
                return NotFound($"Bodega con ID {id} no encontrada.");
            }

            return bodega;
        }

        // Agregar una nueva bodega
        [HttpPost]
        public async Task<ActionResult<Bodega>> PostBodega(Bodega bodega)
        {
            try
            {
                _dbContext.Bodega.Add(bodega);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBodega), new { id = bodega.BodegaID }, bodega);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar la bodega.");
            }
        }

        // Actualizar una bodega
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodega(int id, Bodega bodega)
        {
            if (id != bodega.BodegaID)
            {
                return BadRequest("El ID de la bodega no coincide.");
            }

            _dbContext.Entry(bodega).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodegaExists(id))
                {
                    return NotFound($"Bodega con ID {id} no encontrada.");
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Se actualizó la bodega con ID: {id}");
        }

        // Eliminar una bodega por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodega(int id)
        {
            if (!_dbContext.Bodega.Any())
            {
                return NotFound("No hay bodegas registradas.");
            }

            var bodega = await _dbContext.Bodega.FindAsync(id);
            if (bodega == null)
            {
                return NotFound($"La bodega con ID {id} no existe.");
            }

            _dbContext.Bodega.Remove(bodega);
            await _dbContext.SaveChangesAsync();

            return Ok($"La bodega con ID {id} se eliminó correctamente.");
        }

        private bool BodegaExists(int id)
        {
            return _dbContext.Bodega.Any(e => e.BodegaID == id);
        }
    }
}
