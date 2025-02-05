using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Custom;
using BackendApi.Models;
using BackendApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace BackendApi.Controllers
{
    [Route("api/Usuario")]
    [AllowAnonymous] // Acceder solo usuarios autorizados
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BdGodoyCordobaContext _dbGodoyCordobaContext;
        public UsuarioController(BdGodoyCordobaContext dbGodoyCordobaContext)
        {
            _dbGodoyCordobaContext = dbGodoyCordobaContext;
        }

        // Crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostProducto(Usuario usuario)
        {
            _dbGodoyCordobaContext.Usuarios.Add(usuario);
            await _dbGodoyCordobaContext.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.IdUsuario}, usuario);
        }

        // Obtener la lista de todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _dbGodoyCordobaContext.Usuarios.ToListAsync();
        }

        // Obtener un producto por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _dbGodoyCordobaContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // Actualizar los detalles de un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {

            usuario.IdUsuario = id;
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            _dbGodoyCordobaContext.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _dbGodoyCordobaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _dbGodoyCordobaContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _dbGodoyCordobaContext.Usuarios.Remove(usuario);
            await _dbGodoyCordobaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _dbGodoyCordobaContext.Usuarios.Any(e => e.IdUsuario == id);
        }

    }
}
