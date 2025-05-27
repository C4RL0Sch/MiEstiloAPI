using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiEstiloAPI.DTOs;
using MiEstiloAPI.Models;

namespace MiEstiloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly MiEstiloContext _context;

        public ProductosController(MiEstiloContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProductos()
        {
            return await _context.Productos
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    ImagenUrl = p.ImagenUrl,
                    FechaCreacion = p.FechaCreacion,
                    IdCategoria = p.IdCategoria,
                    Categoria = new CategoriaDTO
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria,
                        Descripcion = p.IdCategoriaNavigation.Descripcion
                    },
                    IdMarca = p.IdMarca,
                    Marca = new MarcaDTO
                    {
                        IdMarca = p.IdMarcaNavigation.IdMarca,
                        NombreMarca = p.IdMarcaNavigation.NombreMarca
                    }
                })
                .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    ImagenUrl = p.ImagenUrl,
                    FechaCreacion = p.FechaCreacion,
                    IdCategoria = p.IdCategoria,
                    Categoria = new CategoriaDTO
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria,
                        Descripcion = p.IdCategoriaNavigation.Descripcion
                    },
                    IdMarca = p.IdMarca,
                    Marca = new MarcaDTO
                    {
                        IdMarca = p.IdMarcaNavigation.IdMarca,
                        NombreMarca = p.IdMarcaNavigation.NombreMarca
                    },
                    /*Caracteristicas = p.ProductoCaracteristicas.Select(pc => new CaracteristicaDTO
                    {
                        IdCaracteristica = pc.IdCaracteristica,
                        NombreCaracteristica = pc.IdCaracteristicaNavigation.NombreCaracteristica
                    }).ToList()*/
                }
                )
                .FirstOrDefaultAsync(p=>p.IdProducto==id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] ProductoDTO producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            var prod = await _context.Productos.FindAsync(id);

            if (prod==null)
            {
                return NotFound();
            }

            prod.NombreProducto = producto.NombreProducto;
            prod.Descripcion = producto.Descripcion;
            prod.Precio = producto.Precio;
            prod.IdCategoria = producto.IdCategoria;
            prod.IdMarca = producto.IdMarca;
            prod.ImagenUrl = producto.ImagenUrl;

            _context.Entry(prod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] ProductoNuevoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                IdCategoria = dto.IdCategoria,
                IdMarca = dto.IdMarca,
                ImagenUrl = dto.ImagenUrl,
                FechaCreacion = DateTime.Now
            };

            if (dto.CaracteristicasIds != null && dto.CaracteristicasIds.Any())
            {
                foreach (var idCaracteristica in dto.CaracteristicasIds)
                {
                    producto.ProductoCaracteristicas.Add(new ProductoCaracteristica
                    {
                        IdCaracteristica = idCaracteristica
                    });
                }
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetByCategory/{idCategoria}")]
        public async Task<ActionResult<List<ProductoDTO>>> GetByCategoria(int idCategoria)
        {
            var productos = await _context.Productos
                .Where(p => p.IdCategoria == idCategoria)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    ImagenUrl = p.ImagenUrl,
                    FechaCreacion = p.FechaCreacion,
                    IdCategoria = p.IdCategoria,
                    Categoria = new CategoriaDTO
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria,
                        Descripcion = p.IdCategoriaNavigation.Descripcion
                    },
                    IdMarca = p.IdMarca,
                    Marca = new MarcaDTO
                    {
                        IdMarca = p.IdMarcaNavigation.IdMarca,
                        NombreMarca = p.IdMarcaNavigation.NombreMarca
                    }
                })
                .ToListAsync();

            return productos;
        }

        [HttpGet("GetByMarca/{idMarca}")]
        public async Task<ActionResult<List<ProductoDTO>>> GetByMarca(int idMarca)
        {
            var productos = await _context.Productos
                .Where(p => p.IdMarca == idMarca)
                .Select(p => new ProductoDTO
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    ImagenUrl = p.ImagenUrl,
                    FechaCreacion = p.FechaCreacion,
                    IdCategoria = p.IdCategoria,
                    Categoria = new CategoriaDTO
                    {
                        IdCategoria = p.IdCategoriaNavigation.IdCategoria,
                        NombreCategoria = p.IdCategoriaNavigation.NombreCategoria,
                        Descripcion = p.IdCategoriaNavigation.Descripcion
                    },
                    IdMarca = p.IdMarca,
                    Marca = new MarcaDTO
                    {
                        IdMarca = p.IdMarcaNavigation.IdMarca,
                        NombreMarca = p.IdMarcaNavigation.NombreMarca
                    }
                })
                .ToListAsync();

            return productos;
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
