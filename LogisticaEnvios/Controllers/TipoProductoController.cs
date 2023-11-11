using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly TipoProductoContext _dbContext;

        public TipoProductoController(TipoProductoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProducto>>> GetTipoProductos()
        {
            if (!_dbContext.TipoProducto.Any())
            {
                return NotFound("No hay tipos de producto disponibles.");
            }

            return await _dbContext.TipoProducto.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProducto>> GetTipoProducto(int id)
        {
            if (!_dbContext.TipoProducto.Any())
            {
                return NotFound("No hay tipos de producto disponibles.");
            }

            var tipoProducto = await _dbContext.TipoProducto.FindAsync(id);

            if (tipoProducto == null)
            {
                return NotFound($"Tipo de producto con ID {id} no encontrado.");
            }

            return tipoProducto;
        }

        [HttpPost]
        public async Task<ActionResult<TipoProducto>> PostTipoProducto(TipoProducto tipoProducto)
        {
            try
            {
                if (_dbContext.TipoProducto.Any(tp => tp.TipoProductoID == tipoProducto.TipoProductoID))
                {
                    return BadRequest($"Ya existe un tipo de producto con ID {tipoProducto.TipoProductoID}.");
                }

                _dbContext.TipoProducto.Add(tipoProducto);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTipoProducto), new { id = tipoProducto.TipoProductoID }, tipoProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el tipo de producto.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProducto(int id, TipoProducto tipoProducto)
        {
            if (id != tipoProducto.TipoProductoID)
            {
                return BadRequest("El ID debe ser el mismo");
            }

            _dbContext.Entry(tipoProducto).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Se actualizó el tipo de producto con ID: {id}");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProducto(int id)
        {
            if (_dbContext.TipoProducto == null)
            {
                return NotFound("No hay tipos de producto registrados");
            }

            var tipoProducto = await _dbContext.TipoProducto.FindAsync(id);

            if (tipoProducto == null)
            {
                return NotFound($"El tipo de producto con ID {id} no existe");
            }

            _dbContext.TipoProducto.Remove(tipoProducto);
            await _dbContext.SaveChangesAsync();

            return Ok($"El tipo de producto con ID {id} se eliminó correctamente");
        }

        private bool TipoProductoExists(int id)
        {
            return _dbContext.TipoProducto.Any(tp => tp.TipoProductoID == id);
        }
    }
}

