using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Proveedor.Proveedor;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Proveedor
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly TokesContext _context;

        public ProveedorController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/Proveedor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tokes.Entidades.Proveedor>>> GetProveedors()
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            return await _context.Proveedors
                .Include(c => c.IdTipoProveedorNavigation)
                .ToListAsync();
        }

        // GET: api/Proveedor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tokes.Entidades.Proveedor>> GetProveedor(int id)
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            var proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // PUT: api/Proveedor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorViewModel req)
        {
            var proveedor = await _context.Proveedors.FirstOrDefaultAsync(c => c.IdProveedor == id);

            if(proveedor == null)
            {
                return NotFound();
            }

            proveedor.IdTipoProveedor = req.IdTipoProveedor;
            proveedor.Nombre = req.Nombre;
            proveedor.Departamento = req.Departamento;
            proveedor.Municipio = req.Municipio;
            proveedor.Direccion = req.Direccion;
            proveedor.Telefono = req.Telefono;

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

        // POST: api/Proveedor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProveedorViewModel>> PostProveedor(ProveedorViewModel req)
        {
          if (_context.Proveedors == null)
          {
              return Problem("Entity set 'TokesContext.Proveedors'  is null.");
          }

            Tokes.Entidades.Proveedor proveedor = new Tokes.Entidades.Proveedor
            {
                IdTipoProveedor = req.IdTipoProveedor,
                Nombre = req.Nombre,
                Departamento = req.Departamento,
                Municipio = req.Municipio,
                Direccion = req.Direccion,
                Telefono = req.Telefono,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true
            };
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.IdProveedor }, proveedor);
        }

        // DELETE: api/Proveedor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            if (_context.Proveedors == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedors?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }
    }
}
