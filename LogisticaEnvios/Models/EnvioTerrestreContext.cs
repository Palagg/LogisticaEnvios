using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class EnvioTerrestreContext : DbContext
    {
        public EnvioTerrestreContext(DbContextOptions<EnvioTerrestreContext> options) : base(options)
        {

        }
        public DbSet<EnvioTerrestre> EnvioTerrestre { get; set; }
        public DbSet<Bodega> Bodega { get; set; }
        public DbSet<PlanDeEntrega> PlanDeEntrega { get; set; }
    }
}
