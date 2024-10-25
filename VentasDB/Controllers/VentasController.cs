using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentasDB.Models;

namespace VentasDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public VentasController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        {
            return await _context.Ventas
                .Select(c => new Venta
                {
                    IdVenta = c.IdVenta,
                    FechaVenta = c.FechaVenta,
                    TipoVenta = c.TipoVenta,
                    TotalVenta = c.TotalVenta
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {
            var venta = await _context.Ventas
                .Select(c => new Venta
                {
                    IdVenta = c.IdVenta,
                    FechaVenta = c.FechaVenta,
                    TipoVenta = c.TipoVenta,
                    TotalVenta = c.TotalVenta
                })
                .FirstOrDefaultAsync(c => c.IdVenta == id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Venta>> PostVenta([FromBody] VentaDTO ventaDTO)
        {
            var venta = new Venta
            {
                FechaVenta = ventaDTO.FechaVenta,
                TipoVenta = ventaDTO.TipoVenta,
                TotalVenta = ventaDTO.TotalVenta
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, [FromBody] VentaDTO ventaDTO)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            venta.FechaVenta = ventaDTO.FechaVenta;
            venta.TipoVenta = ventaDTO.TipoVenta;
            venta.TotalVenta = ventaDTO.TotalVenta;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasExists(id))
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
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentasExists(int id)
        {
            return _context.Ventas.Any(e => e.IdVenta == id);
        }
    }

    public class VentaDTO
    {
        public DateTime? FechaVenta { get; set; }

        public string? TipoVenta { get; set; }

        public decimal? TotalVenta { get; set; }
    }
}