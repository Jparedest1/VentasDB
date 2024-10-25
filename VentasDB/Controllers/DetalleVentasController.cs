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
    public class DetalleVentasController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public DetalleVentasController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetalleVentas()
        {
            return await _context.DetalleVentas
                .Select(c => new DetalleVenta
                {
                    IdDetalleVenta = c.IdDetalleVenta,
                    IdVenta = c.IdVenta,
                    IdProducto = c.IdProducto,
                    Cantidad = c.Cantidad,
                    PrecioUnitario = c.PrecioUnitario,
                    Subtotal = c.Subtotal
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(int id)
        {
            var detalleventas = await _context.DetalleVentas
                .Select(c => new DetalleVenta
                {
                    IdDetalleVenta = c.IdDetalleVenta,
                    IdVenta = c.IdVenta,
                    IdProducto = c.IdProducto,
                    Cantidad = c.Cantidad,
                    PrecioUnitario = c.PrecioUnitario,
                    Subtotal = c.Subtotal
                })
                .FirstOrDefaultAsync(c => c.IdDetalleVenta == id);

            if (detalleventas == null)
            {
                return NotFound();
            }

            return detalleventas;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<DetalleVenta>> PostDetalleVenta([FromBody] DetalleVentaDTO detalleventaDTO)
        {
            var detalleventas = new DetalleVenta
            {
                IdVenta = detalleventaDTO.IdVenta,
                IdProducto = detalleventaDTO.IdProducto,
                Cantidad = detalleventaDTO.Cantidad,
                PrecioUnitario = detalleventaDTO.PrecioUnitario,
                Subtotal = detalleventaDTO.Subtotal
            };

            _context.DetalleVentas.Add(detalleventas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleVentas), new { id = detalleventas.IdDetalleVenta }, detalleventas);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, [FromBody] DetalleVentaDTO detalleventaDTO)
        {
            var detalleventas = await _context.DetalleVentas.FindAsync(id);
            if (detalleventas == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            detalleventas.IdVenta = detalleventaDTO.IdVenta;
            detalleventas.IdProducto = detalleventaDTO.IdProducto;
            detalleventas.Cantidad = detalleventaDTO.Cantidad;
            detalleventas.PrecioUnitario = detalleventaDTO.PrecioUnitario;
            detalleventas.Subtotal = detalleventaDTO.Subtotal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaExists(id))
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
        public async Task<IActionResult> DeleteDetalleVenta(int id)
        {
            var detalleventa = await _context.DetalleVentas.FindAsync(id);
            if (detalleventa == null)
            {
                return NotFound();
            }

            _context.DetalleVentas.Remove(detalleventa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetalleVentas.Any(e => e.IdDetalleVenta == id);
        }
    }

    public class DetalleVentaDTO
    {
        public int? IdVenta { get; set; }

        public int? IdProducto { get; set; }

        public int? Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

        public decimal? Subtotal { get; set; }
    }
}