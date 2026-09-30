using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Producto.CategoriaProducto;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Producto
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProductoController : ControllerBase
    {
        private readonly TokesContext _context;

        public CategoriaProductoController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaProducto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProducto>>> GetCategoriaProductos()
        {
          if (_context.CategoriaProductos == null)
          {
              return NotFound();
          }
            return await _context.CategoriaProductos.ToListAsync();
        }

        // GET: api/CategoriaProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaProducto>> GetCategoriaProducto(int id)
        {
          if (_context.CategoriaProductos == null)
          {
              return NotFound();
          }
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);

            if (categoriaProducto == null)
            {
                return NotFound();
            }

            return categoriaProducto;
        }

        // PUT: api/CategoriaProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaProducto(int id, CategoriaProductoViewModel req)
        {
            var catProd = await _context.CategoriaProductos.FirstOrDefaultAsync(c => c.IdCategoriaProducto == id);

            if(catProd == null)
            {
                return NotFound();
            }

            catProd.Nombre = req.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaProductoExists(id))
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

        // POST: api/CategoriaProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoriaProducto(CategoriaProductoViewModel req)
        {
          if (_context.CategoriaProductos == null)
          {
              return Problem("Entity set 'TokesContext.CategoriaProductos'  is null.");
          }
            CategoriaProducto categoriaProducto = new CategoriaProducto
            {
                Nombre = req.Nombre,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true
            };
            _context.CategoriaProductos.Add(categoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProducto", new { id = categoriaProducto.IdCategoriaProducto }, categoriaProducto);
        }

        // DELETE: api/CategoriaProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaProducto(int id)
        {
            if (_context.CategoriaProductos == null)
            {
                return NotFound();
            }
            var categoriaProducto = await _context.CategoriaProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return NotFound();
            }

            _context.CategoriaProductos.Remove(categoriaProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaProductoExists(int id)
        {
            return (_context.CategoriaProductos?.Any(e => e.IdCategoriaProducto == id)).GetValueOrDefault();
        }
    }
}
