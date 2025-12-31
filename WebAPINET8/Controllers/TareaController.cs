using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPINET8.Database;
using WebAPINET8.Models;

namespace WebAPINET8.Controllers
{
    [Route("api/[controller]")]  // Ruta base: /api/Tarea
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Conexión a BD

        public TareaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tarea/Usuario/5
        // Obtiene todas las tareas de un usuario específico
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var tareas = await _context.Tareas
                .Where(t => t.IdUsuario == usuarioId)  // Filtra por usuario
                .ToListAsync();

            return Ok(tareas);  // Devuelve lista de tareas
        }

        // POST: api/Tarea
        // Crea una nueva tarea
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Tarea tarea)
        {
            // Verifica que el usuario exista
            var usuario = await _context.Usuarios
                .SingleOrDefaultAsync(u => u.Id == tarea.IdUsuario);

            if (usuario == null)
                return NotFound("El usuario no existe.");

            // Guarda la nueva tarea
            await _context.Tareas.AddAsync(tarea);
            await _context.SaveChangesAsync();

            return Ok(tarea);  // Devuelve la tarea creada
        }

        // PUT: api/Tarea/Estado/5
        // Cambia el estado de completado de una tarea
        [HttpPut("Estado/{id}")]
        public async Task<IActionResult> PutEstado(int id, [FromBody] bool completada)
        {
            var tareaInfo = await _context.Tareas
                .SingleOrDefaultAsync(t => t.Id == id);

            if (tareaInfo == null)
                return NotFound();  // Tarea no existe

            tareaInfo.Completada = completada;  // Actualiza estado
            _context.Attach(tareaInfo);
            await _context.SaveChangesAsync();

            return Ok(tareaInfo);  // Devuelve tarea actualizada
        }

        // DELETE: api/Tarea/5
        // Elimina una tarea por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tareaInfo = await _context.Tareas
                .SingleOrDefaultAsync(t => t.Id == id);

            if (tareaInfo == null)
                return NotFound();  // Tarea no existe

            _context.Tareas.Remove(tareaInfo);  // Elimina tarea
            await _context.SaveChangesAsync();

            return Ok();  // Confirmación sin datos
        }
    }
}