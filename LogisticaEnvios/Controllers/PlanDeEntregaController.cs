using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanDeEntregaController : ControllerBase
    {
        private readonly PlanDeEntregaContext _dbContext;

        public PlanDeEntregaController(PlanDeEntregaContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Obtener los planes de entrega
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDeEntrega>>> GetPlanDeEntrega()
        {
            if (!_dbContext.PlanDeEntrega.Any())
            {
                return NotFound("No hay planes de entrega disponibles.");
            }

            return await _dbContext.PlanDeEntrega.ToListAsync();
        }

        //Obtener plan de entrega por id
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDeEntrega>> GetPlanDeEntrega(int id)
        {
            if (!_dbContext.PlanDeEntrega.Any())
            {
                return NotFound("No hay planes de entrega disponibles.");
            }

            var planDeEntrega = await _dbContext.PlanDeEntrega.FindAsync(id);

            if (planDeEntrega == null)
            {
                return NotFound($"Plan de entrega con ID {id} no encontrado.");
            }

            return planDeEntrega;
        }

        //Agregar plan de entrega
        [HttpPost]
        public async Task<ActionResult<PlanDeEntrega>> PostPlanDeEntrega(PlanDeEntrega planDeEntrega)
        {
            try
            {
                if (!_dbContext.TipoProducto.Any(tp => tp.TipoProductoID == planDeEntrega.TipoProductoID))
                {
                    return BadRequest($"No existe TipoProducto con ID {planDeEntrega.TipoProductoID}.");
                }

                if (!_dbContext.Cliente.Any(c => c.Cedula == planDeEntrega.ClienteCedula))
                {
                    return BadRequest($"No existe Cliente con cédula {planDeEntrega.ClienteCedula}.");
                }

                _dbContext.PlanDeEntrega.Add(planDeEntrega);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlanDeEntrega), new { id = planDeEntrega.PlanID }, planDeEntrega);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error inesperado al agregar el plan de entrega: {ex.Message}");
            }
        }

        // Actualizar Plan de Entrega
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanDeEntrega(int id, PlanDeEntrega planDeEntrega)
        {
            if (id != planDeEntrega.PlanID)
            {
                return BadRequest();
            }

            if (!_dbContext.PlanDeEntrega.Any(pe => pe.PlanID == id))
            {
                return NotFound($"Plan de entrega con ID {id} no encontrado.");
            }

            try
            {
                if (!_dbContext.TipoProducto.Any(tp => tp.TipoProductoID == planDeEntrega.TipoProductoID))
                {
                    return BadRequest($"No existe TipoProducto con ID {planDeEntrega.TipoProductoID}.");
                }

                if (!_dbContext.Cliente.Any(c => c.Cedula == planDeEntrega.ClienteCedula))
                {
                    return BadRequest($"No existe Cliente con cédula {planDeEntrega.ClienteCedula}.");
                }

                _dbContext.Entry(planDeEntrega).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanDeEntregaExists(id))
                {
                    return NotFound($"Plan de entrega con ID {id} no encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Eliminar Plan de Entrega
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanDeEntrega(int id)
        {
            if (!_dbContext.PlanDeEntrega.Any())
            {
                return NotFound("No hay planes de entrega registrados.");
            }

            var planDeEntrega = await _dbContext.PlanDeEntrega.FindAsync(id);
            if (planDeEntrega == null)
            {
                return NotFound($"Plan de entrega con ID {id} no encontrado.");
            }

            _dbContext.PlanDeEntrega.Remove(planDeEntrega);
            await _dbContext.SaveChangesAsync();

            return Ok($"El plan de entrega con ID {id} se eliminó correctamente.");
        }

        private bool PlanDeEntregaExists(int id)
        {
            return _dbContext.PlanDeEntrega.Any(e => e.PlanID == id);
        }
    }
}
