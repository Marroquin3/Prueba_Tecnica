using Microsoft.EntityFrameworkCore;  // Esta línea importa las herramientas para trabajar con bases de datos
using WebAPINET8.Database;           // Esta línea importa nuestras propias clases de base de datos

// Crea el "constructor" de la aplicación web
var builder = WebApplication.CreateBuilder(args);

// PASO 1: Configurar los servicios de la aplicación

// Agrega soporte para controladores (archivos que manejan las peticiones HTTP como GET, POST)
builder.Services.AddControllers();

// OBTIENE la cadena de conexión a la base de datos desde el archivo de configuración
// Esta cadena contiene información como: servidor, nombre de base de datos, usuario, contraseña
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura la conexión a la base de datos usando Entity Framework Core
// ApplicationDbContext es nuestra clase que maneja la base de datos
// UseSqlServer indica que usaremos SQL Server como motor de base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)  // Usa la cadena de conexión que obtuvimos arriba
);

// Agrega soporte para documentación automática de la API (Endpoints API)
builder.Services.AddEndpointsApiExplorer();

// Agrega soporte para Swagger/OpenAPI (herramienta para probar y documentar la API)
builder.Services.AddSwaggerGen();

// PASO 2: Construir la aplicación
var app = builder.Build();

// PASO 3: Configurar el "pipeline" de peticiones HTTP
// (El orden de estas líneas es importante porque las peticiones pasan por ellas en secuencia)

// Redirige automáticamente las peticiones HTTP a HTTPS (para mayor seguridad)
app.UseHttpsRedirection();

// Habilita la autorización (control de quién puede acceder a qué)
app.UseAuthorization();

// Mapea los controladores a rutas URL (ej: /api/usuarios va al controlador Usuarios)
app.MapControllers();

// Habilita Swagger (documentación interactiva de la API)
app.UseSwagger();

// Configura la interfaz web de Swagger en la ruta /swagger
// "Prueba API" es el título que aparecerá en la documentación
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API"));

// PASO 4: Iniciar la aplicación y esperar peticiones
app.Run();  // La aplicación comienza a escuchar en los puertos configurados (ej: localhost:5004)