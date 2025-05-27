using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using MiEstiloAPI.DTOs;
using MiEstiloAPI.Models;
using MiEstiloAPI.Services;
using System;

namespace MiEstiloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MiEstiloContext _context;
        private readonly JwtService _jwtService;

        public AuthController(MiEstiloContext context, JwtService jwtService)
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
                Telefono = user.Telefono
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(usuario.IdUsuario.ToString(), usuario.Email);

            HttpContext.Response.Cookies.Append("jwt_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // solo en HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new { message = "Login exitoso" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contraseña, usuario.Contraseña))
                return Unauthorized("Credenciales inválidas");

            var token = _jwtService.GenerateToken(usuario.IdUsuario.ToString(), usuario.Email);
            return Ok(new { token });
        }
    }
}
