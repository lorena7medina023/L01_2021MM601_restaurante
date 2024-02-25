using Microsoft.EntityFrameworkCore;

namespace L01_2021MM601.Models
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {

        }

        public DbSet <Clientes> Clientes { get; set; } 
        public DbSet <Pedidos> Pedidos { get; set; }
        public DbSet <Platos> Platos { get; set; }
        public DbSet <Motoristas> Motoristas { get; set;}
    }
}
