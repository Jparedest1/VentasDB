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
    public class BitacoraController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public BitacoraController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BitacoraEntrega>>> GetBitacoraEntregas()
        {
            return await _context.BitacoraEntregas
                .Select(c => new BitacoraEntrega
                {
                    IdBitacora = c.IdBitacora,
                    FechaHoraRegistro = c.FechaHoraRegistro,
                    Descripcion = c.Descripcion,
                    Usuario = c.Usuario
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BitacoraEntrega>>> GetBitacoraEntrega(int id)
        {
            var bitacora = await _context.BitacoraEntregas
                .Select(c => new BitacoraEntrega
                {
                    IdBitacora = c.IdBitacora,
                    FechaHoraRegistro = c.FechaHoraRegistro,
                    Descripcion = c.Descripcion,
                    Usuario = c.Usuario
                })
                .FirstOrDefaultAsync(c => c.IdBitacora == id);

            if (bitacora == null)
            {
                return NotFound();
            }

            return Ok(bitacora);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<BitacoraEntrega>> PostBitacoraEntrega([FromBody] BitacoraEntregaDTO bitacoraDTO)
        {
            var bitacora = new BitacoraEntrega
            {
                FechaHoraRegistro = bitacoraDTO.FechaHoraRegistro,
                Descripcion = bitacoraDTO.Descripcion,
                Usuario = bitacoraDTO.Usuario
            };

            _context.BitacoraEntregas.Add(bitacora);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBitacoraEntrega), new { id = bitacora.IdBitacora }, bitacora);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBitacoraEntrega(int id, [FromBody] BitacoraEntregaDTO bitacoraDTO)
        {
            var bitacora = await _context.BitacoraEntregas.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            bitacora.FechaHoraRegistro = bitacoraDTO.FechaHoraRegistro;
            bitacora.Descripcion = bitacoraDTO.Descripcion;
            bitacora.Usuario = bitacoraDTO.Usuario;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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
        public async Task<IActionResult> DeleteBitacoraEntrega(int id)
        {
            var bitacora = await _context.BitacoraEntregas.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            _context.BitacoraEntregas.Remove(bitacora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BitacoraExists(int id)
        {
            return _context.BitacoraEntregas.Any(e => e.IdBitacora == id);
        }
    }

    public class BitacoraEntregaDTO
    {
        public DateTime? FechaHoraRegistro { get; set; }

        public string? Descripcion { get; set; }

        public string? Usuario { get; set; }
    }
}