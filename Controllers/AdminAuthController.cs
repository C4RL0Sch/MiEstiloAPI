using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiEstiloAPI.DTOs;
using MiEstiloAPI.Models;
using MiEstiloAPI.Services;

namespace MiEstiloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly MiEstiloContext _context;
        private readonly JwtService _jwtService;

        public AdminAuthController(MiEstiloContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Usuario user)
        {
            if (_context.Usuarios.Any(u => u.Email == user.Email))
                return BadRequest("El email ya está registrado");

            var usuario = new Usuario
            {
                Nombre = user.Nombre,
                Email = user.Email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(user.Contraseña),
                Telefono = user.Telefono,
                Rol = "admin"
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro exitoso" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contraseña, usuario.Contraseña) || usuario.Rol != "admin")
                return Unauthorized("Credenciales inválidas");

            var accesstoken = _jwtService.GenerateToken(usuario.IdUsuario.ToString(), usuario.Email, usuario.Rol);
            var refreshTokenValue = _jwtService.GenerateRefreshToken();

            var refreshToken = new RefreshToken
            {
                IdUsuario = usuario.IdUsuario,
                Token = refreshTokenValue,
                Expira = DateTime.UtcNow.AddDays(1)
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            HttpContext.Response.Cookies.Append("jwt_token", accesstoken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // solo en HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            HttpContext.Response.Cookies.Append("refresh_token", refreshTokenValue, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = refreshToken.Expira
            });

            return Ok(new { message = "Login exitoso" });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized();
            var token = await _context.RefreshTokens
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.Revocado);

            if (token == null || token.Expira < DateTime.UtcNow)
                return Unauthorized();

            // Generar nuevo access token
            var newAccessToken = _jwtService.GenerateToken(token.IdUsuario.ToString(), token.IdUsuarioNavigation.Email, token.IdUsuarioNavigation.Rol);

            HttpContext.Response.Cookies.Append("jwt_token", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new { message = "Token renovado" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (refreshToken != null)
            {
                var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
                if (token != null)
                {
                    token.Revocado = true;
                    await _context.SaveChangesAsync();
                }
            }

            Response.Cookies.Delete("jwt_token");
            Response.Cookies.Delete("refresh_token");
            return Ok(new { message = "Sesión cerrada" });

            /*
            // Obtener el claim 'sub' (userId)
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            // Obtener el claim 'email'
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

            return Ok(new
            {
                UserId = userId,
                Email = email
            });*/
        }
    }
}
