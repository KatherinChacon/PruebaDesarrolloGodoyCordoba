using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendApi.Custom;
using BackendApi.Models;
using BackendApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous] // Acceder sin que el usuario este autenticado
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly BdGodoyCordobaContext _dbGodoyCordobaContext;
        private readonly Utilidades _utilidades;
        public AccesoController(BdGodoyCordobaContext dbGodoyCordobaContext, Utilidades utilidades)
        {
            _dbGodoyCordobaContext = dbGodoyCordobaContext;
            _utilidades = utilidades;
        }

        // Crear registro
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(SessionDTO objeto)
        {
            var modeloSesion = new Sesion
            {
                Correo = objeto.Correo,
                Contrasena = _utilidades.encriptarSHA256(objeto.Contrasena),
            };

            await _dbGodoyCordobaContext.AddAsync(modeloSesion);
            await _dbGodoyCordobaContext.SaveChangesAsync(); // Guardar cambios

            if(modeloSesion.IdSesion != 0)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = true});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        // Logue apartir de usuarios creados
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioLogueo = await _dbGodoyCordobaContext.Usuarios
                .Where(u => u.Correo == objeto.Correo)
                .FirstOrDefaultAsync();

            // Buscar usuario
            var usuarioEncontrado = await _dbGodoyCordobaContext.Sesions
                .Where(u =>
                u.Correo == objeto.Correo &&
                u.Contrasena == _utilidades.encriptarSHA256(objeto.Contrasena)
                ).FirstOrDefaultAsync();            

            if(usuarioLogueo != null)
            {
                usuarioLogueo.FechaAcceso = DateTime.UtcNow;
                await _dbGodoyCordobaContext.SaveChangesAsync();
            }

            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = false, token = ""});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado)});
        }
    }
}
