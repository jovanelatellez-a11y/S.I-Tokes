using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Cliente.CategoriaCliente;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Cliente
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaClienteController : ControllerBase
    {
        private readonly TokesContext _context;

        public CategoriaClienteController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaCliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaCliente>>> GetCategoriaClientes()
        {
            if (_context.CategoriaClientes == null)
            {
                return NotFound();
            }
            return await _context.CategoriaClientes.ToListAsync();
        }

        // GET: api/CategoriaCliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaCliente>> GetCategoriaCliente(int id)
        {
            if (_context.CategoriaClientes == null)
            {
                return NotFound();
            }
            var categoriaCliente = await _context.CategoriaClientes.FindAsync(id);

            if (categoriaCliente == null)
            {
                return NotFound();
            }

            return categoriaCliente;
        }

        // PUT: api/CategoriaCliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaCliente(int id, CategoriaClienteViewModel req)
        {

            var cat = await _context.CategoriaClientes.FirstOrDefaultAsync(c => c.IdCategoriaCliente == id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.Nombre = req.Nombre;
            cat.Descripcion = req.Descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaClienteExists(id))
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

        // POST: api/CategoriaCliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaClienteViewModel>> PostCategoriaCliente(CategoriaClienteViewModel req)
        {
            if (_context.CategoriaClientes == null)
            {
                return Problem("Entity set 'TokesContext.CategoriaClientes'  is null.");
            }

            CategoriaCliente categoriaCliente = new CategoriaCliente
            {
                Nombre = req.Nombre,
                Descripcion = req.Descripcion,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true,
                FechaRegistro = DateTime.Now
            };
            _context.CategoriaClientes.Add(categoriaCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaCliente", new { id = categoriaCliente.IdCategoriaCliente }, categoriaCliente);
        }

        // DELETE: api/CategoriaCliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaCliente(int id)
        {
            if (_context.CategoriaClientes == null)
            {
                return NotFound();
            }
            var categoriaCliente = await _context.CategoriaClientes.FindAsync(id);
            if (categoriaCliente == null)
            {
                return NotFound();
            }

            _context.CategoriaClientes.Remove(categoriaCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaClienteExists(int id)
        {
            return (_context.CategoriaClientes?.Any(e => e.IdCategoriaCliente == id)).GetValueOrDefault();
        }
    }
}
