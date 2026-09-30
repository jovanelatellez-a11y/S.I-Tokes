using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Producto.ProveedorProducto;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Producto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorProductoController : ControllerBase
    {
        private readonly TokesContext _context;

        public ProveedorProductoController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/ProveedorProducto
        [HttpGet]
        public async Task<IActionResult> GetProveedorProductos()
        {
          if (_context.ProveedorProductos == null)
          {
              return NotFound();
          }

          var result = await _context.ProveedorProductos
                .Select(c => new
                {
                    c.IdProveedorProducto,
                    c.IdProveedor,
                    Proveedor = c.IdProveedorNavigation.Nombre,
                    c.IdProducto,
                    Producto = c.IdProductoNavigation.Nombre,
                    c.Observaciones,
                    c.Predeterminado,
                    c.FechaRegistro,
                    c.UsuarioRegistro,
                    c.Estado
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/ProveedorProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorProducto>> GetProveedorProducto(int id)
        {
          if (_context.ProveedorProductos == null)
          {
              return NotFound();
          }
            var proveedorProducto = await _context.ProveedorProductos.FindAsync(id);

            if (proveedorProducto == null)
            {
                return NotFound();
            }

            return proveedorProducto;
        }

        // PUT: api/ProveedorProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedorProducto(int id, ProveedorProductoViewModel req)
        {
            var provProd = await _context.ProveedorProductos.FirstOrDefaultAsync(c => c.IdProveedorProducto == id);

            if(provProd == null)
            {
                return NotFound();
            }

            provProd.IdProveedor = req.IdProveedor;
            provProd.IdProducto = req.IdProducto;
            provProd.Observaciones = req.Observaciones;
            provProd.Predeterminado = req.Predeterminado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorProductoExists(id))
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

        // POST: api/ProveedorProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProveedorProducto>> PostProveedorProducto(ProveedorProductoViewModel req)
        {
          if (_context.ProveedorProductos == null)
          {
              return Problem("Entity set 'TokesContext.ProveedorProductos'  is null.");
          }

          ProveedorProducto proveedorProducto = new ProveedorProducto
          {
              IdProveedor = req.IdProveedor,
              IdProducto = req.IdProducto,
              Observaciones = req.Observaciones,
              Predeterminado = req.Predeterminado,
              FechaRegistro = DateTime.Now,
              UsuarioRegistro = req.UsuarioRegistro,
              Estado = true
          };
            _context.ProveedorProductos.Add(proveedorProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedorProducto", new { id = proveedorProducto.IdProveedorProducto }, proveedorProducto);
        }

        // DELETE: api/ProveedorProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedorProducto(int id)
        {
            if (_context.ProveedorProductos == null)
            {
                return NotFound();
            }
            var proveedorProducto = await _context.ProveedorProductos.FindAsync(id);
            if (proveedorProducto == null)
            {
                return NotFound();
            }

            _context.ProveedorProductos.Remove(proveedorProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorProductoExists(int id)
        {
            return (_context.ProveedorProductos?.Any(e => e.IdProveedorProducto == id)).GetValueOrDefault();
        }
    }
}
