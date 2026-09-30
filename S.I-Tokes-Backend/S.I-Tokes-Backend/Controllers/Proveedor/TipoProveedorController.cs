using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Proveedor.TipoProveedor;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Proveedor
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProveedorController : ControllerBase
    {
        private readonly TokesContext _context;

        public TipoProveedorController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/TipoProveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProveedor>>> GetTipoProveedors()
        {
          if (_context.TipoProveedors == null)
          {
              return NotFound();
          }
            return await _context.TipoProveedors.ToListAsync();
        }

        // GET: api/TipoProveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProveedor>> GetTipoProveedor(int id)
        {
          if (_context.TipoProveedors == null)
          {
              return NotFound();
          }
            var tipoProveedor = await _context.TipoProveedors.FindAsync(id);

            if (tipoProveedor == null)
            {
                return NotFound();
            }

            return tipoProveedor;
        }

        // PUT: api/TipoProveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProveedor(int id, TipoProveedorViewModel req)
        {
            var tipoProveedor = await _context.TipoProveedors.FirstOrDefaultAsync(c => c.IdTipoProveedor == id);

            if(tipoProveedor == null)
            {
                return NotFound();
            }

            tipoProveedor.Nombre = req.Nombre;
            tipoProveedor.Observaciones = req.Observaciones;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProveedorExists(id))
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

        // POST: api/TipoProveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoProveedor>> PostTipoProveedor(TipoProveedorViewModel req)
        {
          if (_context.TipoProveedors == null)
          {
              return Problem("Entity set 'TokesContext.TipoProveedors'  is null.");
          }

            TipoProveedor tipoProveedor = new TipoProveedor
            {
                Nombre = req.Nombre,
                Observaciones = req.Observaciones,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true
            };
            _context.TipoProveedors.Add(tipoProveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoProveedor", new { id = tipoProveedor.IdTipoProveedor }, tipoProveedor);
        }

        // DELETE: api/TipoProveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProveedor(int id)
        {
            if (_context.TipoProveedors == null)
            {
                return NotFound();
            }
            var tipoProveedor = await _context.TipoProveedors.FindAsync(id);
            if (tipoProveedor == null)
            {
                return NotFound();
            }

            _context.TipoProveedors.Remove(tipoProveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProveedorExists(int id)
        {
            return (_context.TipoProveedors?.Any(e => e.IdTipoProveedor == id)).GetValueOrDefault();
        }
    }
}
