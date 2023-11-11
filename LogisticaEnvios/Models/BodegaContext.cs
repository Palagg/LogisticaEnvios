using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class BodegaContext : DbContext
    {
        public BodegaContext(DbContextOptions<BodegaContext> options) : base(options)
        {

        }
        public DbSet<Bodega> Bodega { get; set; }
    }
}
