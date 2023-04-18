using Microsoft.EntityFrameworkCore;

namespace WebApiTransporte.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Sucursal> sucursal { get; set; }
        public DbSet<Colaborador> colaborador { get; set; }
        public DbSet<Transportista> transportista { get; set; }
        public DbSet<Sucursal_Colaborador> sucursal_colaborador { get; set; }
    }

  
}
