using Microsoft.EntityFrameworkCore;
using ServicioOData.Models;

namespace ServicioOData.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Inventario> Inventarios => Set<Inventario>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno-a-muchos: Producto 1 ---- * Inventario
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Inventarios)
                .WithOne(i => i.Producto)
                .HasForeignKey(i => i.ProductoId);

            // Datos iniciales (los mismos tenis del primer parcial)
            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Modelo = "Sabrina 2", Color = "Rosa", Precio = 2499m, ImagenUrl = "sabrina_dos_rosas.jpg" },
                new Producto { Id = 2, Modelo = "LeBron 22", Color = "Blanco", Precio = 3799m, ImagenUrl = "lebron_22_blancos.jpg" },
                new Producto { Id = 3, Modelo = "Curry 12", Color = "Negro", Precio = 3299m, ImagenUrl = "curry12_negros.jpg" },
                new Producto { Id = 4, Modelo = "AE 1", Color = "Blanco", Precio = 2999m, ImagenUrl = "ae_1_blancos.jpg" });

            modelBuilder.Entity<Inventario>().HasData(
                // Sabrina 2: mucho stock -> "Disponible"
                new Inventario { Id = 1, ProductoId = 1, Talla = 25m, Stock = 4 },
                new Inventario { Id = 2, ProductoId = 1, Talla = 26m, Stock = 6 },
                new Inventario { Id = 3, ProductoId = 1, Talla = 27m, Stock = 3 },
                // LeBron 22: pocas piezas -> "Pocas piezas"
                new Inventario { Id = 4, ProductoId = 2, Talla = 27m, Stock = 1 },
                new Inventario { Id = 5, ProductoId = 2, Talla = 28m, Stock = 2 },
                new Inventario { Id = 6, ProductoId = 2, Talla = 29m, Stock = 0 },
                // Curry 12: sin stock -> "Agotado"
                new Inventario { Id = 7, ProductoId = 3, Talla = 26.5m, Stock = 0 },
                new Inventario { Id = 8, ProductoId = 3, Talla = 27.5m, Stock = 0 },
                // AE 1: disponible
                new Inventario { Id = 9, ProductoId = 4, Talla = 26m, Stock = 5 },
                new Inventario { Id = 10, ProductoId = 4, Talla = 27m, Stock = 8 },
                new Inventario { Id = 11, ProductoId = 4, Talla = 28m, Stock = 2 });
        }
    }
}
