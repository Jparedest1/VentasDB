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
    public class NotasCreditoController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public NotasCreditoController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotasCredito>>> GetNotasCreditos()
        {
            return await _context.NotasCreditos
                .Select(c => new NotasCredito
                {
                    IdNotaCredito = c.IdNotaCredito,
                    FechaNotaCredito = c.FechaNotaCredito,
                    TipoNotaCredito = c.TipoNotaCredito,
                    TotalNotaCredito = c.TotalNotaCredito
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotasCredito>> GetNotasCredito(int id)
        {
            var notascredito = await _context.NotasCreditos
                .Select(c => new NotasCredito
                {
                    IdNotaCredito = c.IdNotaCredito,
                    FechaNotaCredito = c.FechaNotaCredito,
                    TipoNotaCredito = c.TipoNotaCredito,
                    TotalNotaCredito = c.TotalNotaCredito
                })
                .FirstOrDefaultAsync(c => c.IdNotaCredito == id);

            if (notascredito == null)
            {
                return NotFound();
            }

            return notascredito;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<NotasCredito>> PostNotasCredito([FromBody] NotasCreditoDTO notascreditoDTO)
        {
            var notascredito = new NotasCredito
            {
                FechaNotaCredito = notascreditoDTO.FechaNotaCredito,
                TipoNotaCredito = notascreditoDTO.TipoNotaCredito,
                TotalNotaCredito = notascreditoDTO.TotalNotaCredito
            };

            _context.NotasCreditos.Add(notascredito);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotasCreditos), new { id = notascredito.IdNotaCredito }, notascredito);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotasCredito(int id, [FromBody] NotasCreditoDTO notascreditoDTO)
        {
            var notascredito = await _context.NotasCreditos.FindAsync(id);
            if (notascredito == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            notascredito.FechaNotaCredito = notascreditoDTO.FechaNotaCredito;
            notascredito.TipoNotaCredito = notascreditoDTO.TipoNotaCredito;
            notascredito.TotalNotaCredito = notascreditoDTO.TotalNotaCredito;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasCreditoExists(id))
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
        public async Task<IActionResult> DeleteNotasCredito(int id)
        {
            var notascredito = await _context.NotasCreditos.FindAsync(id);
            if (notascredito == null)
            {
                return NotFound();
            }

            _context.NotasCreditos.Remove(notascredito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotasCreditoExists(int id)
        {
            return _context.NotasCreditos.Any(e => e.IdNotaCredito == id);
        }
    }

    public class NotasCreditoDTO
    {
        public DateTime? FechaNotaCredito { get; set; }

        public string? TipoNotaCredito { get; set; }

        public decimal? TotalNotaCredito { get; set; }
    }
}