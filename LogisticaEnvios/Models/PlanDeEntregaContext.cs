using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class PlanDeEntregaContext : DbContext
    {
        public PlanDeEntregaContext(DbContextOptions<PlanDeEntregaContext> options) : base(options)
        {

        }
        public DbSet<PlanDeEntrega> PlanDeEntrega { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<TipoProducto> TipoProducto { get; set; }
    }
}
