using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Cliente.Cliente;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Cliente
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly TokesContext _context;

        public ClienteController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tokes.Entidades.Cliente>>> GetClientes()
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            return await _context.Clientes.Include(c => c.IdCategoriaClienteNavigation).ToListAsync();
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tokes.Entidades.Cliente>> GetCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteViewModel req)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.IdCategoriaCliente = req.IdCategoriaCliente;
            cliente.Codigo = req.Codigo;
            cliente.Direccion = req.Direccion;
            cliente.Telefono = req.Telefono;
            cliente.Departamento = req.Departamento;
            cliente.Municipio = req.Municipio;
            cliente.PersonaNatural = req.PersonaNatural;

            _context.Entry(req);

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

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tokes.Entidades.Cliente>> PostCliente(ClienteViewModel req)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'TokesContext.Clientes'  is null.");
            }

            Tokes.Entidades.Cliente cliente = new Tokes.Entidades.Cliente
            {
                IdCategoriaCliente = req.IdCategoriaCliente,
                Codigo = req.Codigo,
                Direccion = req.Direccion,
                Telefono = req.Telefono,
                Departamento = req.Departamento,
                Municipio = req.Municipio,
                PersonaNatural = req.PersonaNatural,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true
            };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
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
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
