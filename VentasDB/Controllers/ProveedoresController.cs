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
    public class ProveedoreController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public ProveedoreController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedore>>> GetProveedores()
        {
            return await _context.Proveedores
                .Select(c => new Proveedore
                {
                    IdProveedor = c.IdProveedor,
                    NombreProveedor = c.NombreProveedor,
                    DireccionProveedor = c.DireccionProveedor,
                    ContactoProveedor = c.ContactoProveedor
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedore>> GetProveedore(int id)
        {
            var proveedor = await _context.Proveedores
                .Select(c => new Proveedore
                {
                    IdProveedor = c.IdProveedor,
                    NombreProveedor = c.NombreProveedor,
                    DireccionProveedor = c.DireccionProveedor,
                    ContactoProveedor = c.ContactoProveedor
                })
                .FirstOrDefaultAsync(c => c.IdProveedor == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Proveedore>> PostProveedore([FromBody] ProveedoreDTO proveedorDTO)
        {
            var proveedor = new Proveedore
            {
                NombreProveedor = proveedorDTO.NombreProveedor,
                DireccionProveedor = proveedorDTO.DireccionProveedor,
                ContactoProveedor = proveedorDTO.ContactoProveedor
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProveedore), new { id = proveedor.IdProveedor }, proveedor);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedore(int id, [FromBody] ProveedoreDTO proveedorDTO)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            proveedor.NombreProveedor = proveedorDTO.NombreProveedor;
            proveedor.DireccionProveedor = proveedorDTO.DireccionProveedor;
            proveedor.ContactoProveedor = proveedorDTO.ContactoProveedor;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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
        public async Task<IActionResult> DeleteProveedore(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }

    public class ProveedoreDTO
    {
        public int IdProveedor { get; set; }

        public string? NombreProveedor { get; set; }

        public string? DireccionProveedor { get; set; }

        public string? ContactoProveedor { get; set; }
    }
}