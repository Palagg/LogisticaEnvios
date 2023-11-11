using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class EnvioMaritimoContext : DbContext
    {
        public EnvioMaritimoContext(DbContextOptions<EnvioMaritimoContext> options) : base(options)
        {

        }
        public DbSet<EnvioMaritimo> EnvioMaritimo { get; set; }
        public DbSet<Puerto> Puerto { get; set; }
        public DbSet<PlanDeEntrega> PlanDeEntrega { get; set; }
    }
}
