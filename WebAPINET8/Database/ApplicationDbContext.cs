using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPINET8.Models;

namespace WebAPINET8.Database
{
    public class ApplicationDbContext : DbContext
    {
        // Tablas de la base de datos
        public DbSet<Usuario> Usuarios { get; set; }  // Tabla Usuarios
        public DbSet<Tarea> Tareas { get; set; }      // Tabla Tareas

        // Constructor: recibe configuración de conexión
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                // Intenta crear la BD si no existe
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null && !dbCreator.CanConnect())
                {
                    if (!dbCreator.CanConnect())  // Verifica conexión (condición duplicada)
                        dbCreator.Create();        // Crea la base de datos

                    if (!dbCreator.HasTables())
                        dbCreator.CreateTables();  // Crea las tablas
                }
            }
            catch (Exception ex)
            {
                // Manejo básico de error
                Console.WriteLine(ex.Message);  // Solo muestra en consola
            }
        }
    }
}