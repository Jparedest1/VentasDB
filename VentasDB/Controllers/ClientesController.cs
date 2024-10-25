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
    public class ClienteController : ControllerBase
    {
        private readonly VentasDBContext _context;

        public ClienteController(VentasDBContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes
                .Select(c => new Cliente
                {
                    IdCliente = c.IdCliente,
                    NombresCliente = c.NombresCliente,
                    ApellidosCliente = c.ApellidosCliente,
                    Nit = c.Nit,
                    DireccionCliente = c.DireccionCliente,
                    CategoriaCliente = c.CategoriaCliente,
                    EstadoCliente = c.EstadoCliente
                })
                .ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .Select(c => new Cliente
                {
                    IdCliente = c.IdCliente,
                    NombresCliente = c.NombresCliente,
                    ApellidosCliente = c.ApellidosCliente,
                    Nit = c.Nit,
                    DireccionCliente = c.DireccionCliente,
                    CategoriaCliente = c.CategoriaCliente,
                    EstadoCliente = c.EstadoCliente
                })
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] ClienteDTO clienteDTO)
        {
            var cliente = new Cliente
            {
                NombresCliente = clienteDTO.NombresCliente,
                ApellidosCliente = clienteDTO.ApellidosCliente,
                Nit = clienteDTO.Nit,
                DireccionCliente = clienteDTO.DireccionCliente,
                CategoriaCliente = clienteDTO.CategoriaCliente,
                EstadoCliente = clienteDTO.EstadoCliente
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del cliente
            cliente.NombresCliente = clienteDTO.NombresCliente;
            cliente.ApellidosCliente = clienteDTO.ApellidosCliente;
            cliente.Nit = clienteDTO.Nit;
            cliente.DireccionCliente = clienteDTO.DireccionCliente;
            cliente.CategoriaCliente = clienteDTO.CategoriaCliente;
            cliente.EstadoCliente = clienteDTO.EstadoCliente;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }

    public class ClienteDTO
    {
        public string? NombresCliente { get; set; }
        public string? ApellidosCliente { get; set; }
        public string? Nit { get; set; }
        public string? DireccionCliente { get; set; }
        public string? CategoriaCliente { get; set; }
        public string? EstadoCliente { get; set; }
    }
}