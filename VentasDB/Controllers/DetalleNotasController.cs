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
    [EnableCors("CorsPolicy")]
    public class DetalleNotasController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public DetalleNotasController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleNotasCredito>>> GetDetalleNotasCreditos()
        {
            return await _context.DetalleNotasCreditos
                .Select(c => new DetalleNotasCredito
                {
                    IdDetalleNotaCredito = c.IdDetalleNotaCredito,
                    Cantidad = c.Cantidad,
                    PrecioUnitario = c.PrecioUnitario,
                    Subtotal = c.Subtotal
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleNotasCredito>> GetDetalleNotasCredito(int id)
        {
            var credito = await _context.DetalleNotasCreditos
                .Select(c => new DetalleNotasCredito
                {
                    IdDetalleNotaCredito = c.IdDetalleNotaCredito,
                    Cantidad = c.Cantidad,
                    PrecioUnitario = c.PrecioUnitario,
                    Subtotal = c.Subtotal
                })
                .FirstOrDefaultAsync(c => c.IdDetalleNotaCredito == id);

            if (credito == null)
            {
                return NotFound();
            }

            return credito;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<DetalleNotasCredito>> PostDetalleNotasCredito([FromBody] DetalleNotasCreditoDTO creditoDTO)
        {
            var credito = new DetalleNotasCredito
            {
                Cantidad = creditoDTO.Cantidad,
                PrecioUnitario = creditoDTO.PrecioUnitario,
                Subtotal = creditoDTO.Subtotal
            };

            _context.DetalleNotasCreditos.Add(credito);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleNotasCredito), new { id = credito.IdDetalleNotaCredito }, credito);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleNotasCredito(int id, [FromBody] DetalleNotasCreditoDTO creditoDTO)
        {
            var credito = await _context.DetalleNotasCreditos.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            credito.Cantidad = creditoDTO.Cantidad;
            credito.PrecioUnitario = creditoDTO.PrecioUnitario;
            credito.Subtotal = creditoDTO.Subtotal;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditoExists(id))
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
        public async Task<IActionResult> DeleteDetalleNotasCredito(int id)
        {
            var credito = await _context.DetalleNotasCreditos.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }

            _context.DetalleNotasCreditos.Remove(credito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditoExists(int id)
        {
            return _context.DetalleNotasCreditos.Any(e => e.IdDetalleNotaCredito == id);
        }
    }

    public class DetalleNotasCreditoDTO
    {
        public int? Cantidad { get; set; }

        public decimal? PrecioUnitario { get; set; }

        public decimal? Subtotal { get; set; }
    }
}