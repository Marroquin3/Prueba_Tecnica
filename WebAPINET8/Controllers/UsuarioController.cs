using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebAPINET8.Database;
using WebAPINET8.Models;

namespace WebAPINET8.Controllers
{
    [Route("api/[controller]")]  // Ruta base: /api/Usuario
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;  // Conexión a BD
        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        // Obtiene todos los usuarios
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _context.Usuarios.ToListAsync();  // Obtiene lista completa
            if (result == null)
                return NotFound();  // No hay usuarios
            return Ok(result);  // Devuelve usuarios
        }

        // GET api/Usuario/5
        // Obtiene un usuario específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();  // Usuario no encontrado
            return Ok(result);  // Devuelve usuario encontrado
        }

        // POST api/Usuario
        // Crea un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);  // Agrega usuario
            await _context.SaveChangesAsync();  // Guarda cambios
            return Ok(usuario);  // Devuelve usuario creado
        }

        // PUT api/Usuario/5
        // Actualiza un usuario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            var usuarioInfo = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id);
            if (usuarioInfo == null)
                return NotFound();  // Usuario no existe

            // Actualiza propiedades
            usuarioInfo.Nombre = usuario.Nombre;
            usuarioInfo.Correo = usuario.Correo;

            _context.Attach(usuarioInfo);
            await _context.SaveChangesAsync();  // Guarda cambios

            return Ok(usuarioInfo);  // Devuelve usuario actualizado
        }

        // DELETE api/Usuario/5
        // Elimina un usuario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuarioInfo = await _context.Usuarios.SingleOrDefaultAsync(x => x.Id == id);
            if (usuarioInfo == null)
                return NotFound();  // Usuario no existe

            _context.Usuarios.Remove(usuarioInfo);  // Elimina usuario
            await _context.SaveChangesAsync();  // Guarda cambios

            return Ok();  // Confirmación sin datos
        }
    }
}