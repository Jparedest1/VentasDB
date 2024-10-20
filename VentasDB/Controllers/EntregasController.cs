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
    public class EntregaPaqueteController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public EntregaPaqueteController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntregaPaquete>>> GetEntregaPaquetes()
        {
            return await _context.EntregaPaquetes
                .Select(c => new EntregaPaquete
                {
                    IdEntrega = c.IdEntrega,
                    FechaEntrega = c.FechaEntrega,
                    EstadoEntrega = c.EstadoEntrega
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntregaPaquete>> GetEntregaPaquete(int id)
        {
            var entregapaquete = await _context.EntregaPaquetes
                .Select(c => new EntregaPaquete
                {
                    IdEntrega = c.IdEntrega,
                    FechaEntrega = c.FechaEntrega,
                    EstadoEntrega = c.EstadoEntrega
                })
                .FirstOrDefaultAsync(c => c.IdEntrega == id);

            if (entregapaquete == null)
            {
                return NotFound();
            }

            return entregapaquete;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<EntregaPaquete>> PostCliente([FromBody] EntregaPaqueteDTO entregapaqueteDTO)
        {
            var entregapaquete = new EntregaPaquete
            {
                FechaEntrega = entregapaqueteDTO.FechaEntrega,
                EstadoEntrega = entregapaqueteDTO.EstadoEntrega
            };

            _context.EntregaPaquetes.Add(entregapaquete);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEntregaPaquete), new { id = entregapaquete.IdEntrega }, entregapaquete);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntregaPaquete(int id, [FromBody] EntregaPaqueteDTO entregapaqueteDTO)
        {
            var entregapaquete = await _context.EntregaPaquetes.FindAsync(id);
            if (entregapaquete == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            entregapaquete.FechaEntrega = entregapaqueteDTO.FechaEntrega;
            entregapaquete.EstadoEntrega = entregapaqueteDTO.EstadoEntrega;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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
        public async Task<IActionResult> DeleteEntregaPaquete(int id)
        {
            var entregapaquete = await _context.EntregaPaquetes.FindAsync(id);
            if (entregapaquete == null)
            {
                return NotFound();
            }

            _context.EntregaPaquetes.Remove(entregapaquete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return _context.EntregaPaquetes.Any(e => e.IdEntrega == id);
        }
    }

    public class EntregaPaqueteDTO
    {
        public DateOnly? FechaEntrega { get; set; }

        public string? EstadoEntrega { get; set; }
    }
}