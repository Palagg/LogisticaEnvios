using Microsoft.EntityFrameworkCore;

namespace LogisticaEnvios.Models
{
    public class ClientesContext : DbContext
    {
        public ClientesContext(DbContextOptions<ClientesContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Cliente { get; set; } 
    }
}
