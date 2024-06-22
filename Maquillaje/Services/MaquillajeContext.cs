using Microsoft.EntityFrameworkCore;
using Maquillaje.Models;

namespace Maquillaje.Services
{
    public class MaquillajeContext : DbContext 
    {
        public MaquillajeContext(DbContextOptions options):base(options) { 

        }
        public DbSet<Producto> Productos { get; set; }
    }
}   
