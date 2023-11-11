using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class PuertoContext : DbContext
    {
        public PuertoContext(DbContextOptions<PuertoContext> options) : base(options)
        {

        }
        public DbSet<Puerto> Puerto { get; set; }
    }
}
