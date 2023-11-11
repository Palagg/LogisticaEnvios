using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class TipoProductoContext : DbContext
    {
        public TipoProductoContext(DbContextOptions<TipoProductoContext> options) : base(options)
        {

        }
        public DbSet<TipoProducto> TipoProducto { get; set; }
    }
}
