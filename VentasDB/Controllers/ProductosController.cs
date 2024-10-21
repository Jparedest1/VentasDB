using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasDB.Models;

namespace VentasDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public ProductoController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .Select(c => new Producto
                {
                    IdProducto = c.IdProducto,
                    Descripcion = c.Descripcion,
                    PrecioUnitario = c.PrecioUnitario,
                    FechaVencimiento = c.FechaVencimiento,
                    UbicacionFisica = c.UbicacionFisica,
                    ExistenciaMinima = c.ExistenciaMinima
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Select(c => new Producto
                {
                    IdProducto = c.IdProducto,
                    Descripcion = c.Descripcion,
                    PrecioUnitario = c.PrecioUnitario,
                    FechaVencimiento = c.FechaVencimiento,
                    UbicacionFisica = c.UbicacionFisica,
                    ExistenciaMinima = c.ExistenciaMinima
                })
                .FirstOrDefaultAsync(c => c.IdProducto == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] ProductoDTO productoDTO)
        {
            var producto = new Producto
            {
                Descripcion = productoDTO.Descripcion,
                PrecioUnitario = productoDTO.PrecioUnitario,
                FechaVencimiento = productoDTO.FechaVencimiento,
                UbicacionFisica = productoDTO.UbicacionFisica,
                ExistenciaMinima = productoDTO.ExistenciaMinima
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, [FromBody] ProductoDTO productoDTO)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            producto.Descripcion = productoDTO.Descripcion;
            producto.PrecioUnitario = productoDTO.PrecioUnitario;
            producto.FechaVencimiento = productoDTO.FechaVencimiento;
            producto.UbicacionFisica = productoDTO.UbicacionFisica;
            producto.ExistenciaMinima = productoDTO.ExistenciaMinima;

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

        // DELETE: api/Cliente/5
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

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }

    public class ProductoDTO
    {
        public string? Descripcion { get; set; }

        public decimal? PrecioUnitario { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public string? UbicacionFisica { get; set; }

        public int? ExistenciaMinima { get; set; }
    }
}