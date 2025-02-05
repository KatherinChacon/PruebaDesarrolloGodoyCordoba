using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendApi.Models;

namespace BackendApi.Custom
{
    public class Utilidades
    {
        // Clase para acceder a infromación de appsettings json
        private readonly IConfiguration _configuration;
        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Metodo para encriptar contraseña
        public string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir Texto
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array en bytes a string
                StringBuilder builder = new StringBuilder();
                // Iterar cada elemento
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();

            }
        }

        // Generar Json Tokens
        public string generarJWT(Sesion modelo) 
        {
            //Crear informacion de usuario para token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.IdSesion.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!)
            };

            // Crear llame de seguridad
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            // Crear credenciales
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credential
            );

            // Escribir el token
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);          
        }        
    }
}
