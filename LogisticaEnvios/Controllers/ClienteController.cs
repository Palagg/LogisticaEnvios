using LogisticaEnvios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesContext _dbContext;

        public ClientesController(ClientesContext dbContext)
        {
            _dbContext = dbContext;
        }
        //obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _dbContext.Cliente.ToListAsync();
        }
        //obtener cliente por cedula
        [HttpGet("{cedula}")]
        public async Task<ActionResult<Cliente>> GetCliente(string cedula)
        {
            if (!_dbContext.Cliente.Any())
            {
                return NotFound("No hay clientes disponibles.");
            }
            var cliente = await _dbContext.Cliente.FindAsync(cedula);
            if (cliente == null)
            {
                return NotFound($"Cliente con cédula {cedula} no encontrado.");
            }

            return cliente;
        }
        //ingresar nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            try
            {
                if (_dbContext.Cliente.Any(c => c.Cedula == cliente.Cedula))
                {
                    return BadRequest($"Ya existe un cliente con la cédula {cliente.Cedula}.");
                }
                _dbContext.Cliente.Add(cliente);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCliente), new { Cedula = cliente.Cedula }, cliente);
            }
            //excepcion
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inesperado al agregar el cliente.");
            }
        }
        //actualizar cliente
        [HttpPut]
        public async Task<IActionResult> PutCliente(string cedula, Cliente cliente)
        {
            if (cedula != cliente.Cedula)
            {
                return BadRequest("La cédula debe ser la misma");
            }
            _dbContext.Entry(cliente).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvaiable(cedula))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Se actualizó el cliente con cédula: "+ cedula);
        }
        private bool BrandAvaiable(string cedula)
        {
            return (_dbContext.Cliente?.Any(x => x.Cedula == cedula)).GetValueOrDefault();
        }
        //eliminar un cliente
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> DeleteBrand(string cedula)
        {
            if (_dbContext.Cliente == null)
            {
                return NotFound("No hay clientes registrados");
            }

            var cliente = await _dbContext.Cliente.FindAsync(cedula);
            if (cliente == null)
            {
                return NotFound($"el cliente con cédula {cedula} no existe");
            }
            _dbContext.Cliente.Remove(cliente);
            await _dbContext.SaveChangesAsync();
            return Ok($"El cliente con cédula {cedula} se eliminó correctamente");
        }
    }
}
