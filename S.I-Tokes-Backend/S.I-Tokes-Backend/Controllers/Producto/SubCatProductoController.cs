using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Producto.SubCatProducto;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Producto
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCatProductoController : ControllerBase
    {
        private readonly TokesContext _context;

        public SubCatProductoController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/SubCatProducto
        [HttpGet]
        public async Task<IActionResult> GetSubCategoriaProds()
        {
          if (_context.SubCategoriaProds == null)
          {
              return NotFound();
          }

          var result = await _context.SubCategoriaProds
                .Select(c => new
                {
                    c.IdSubCatProd,
                    c.Nombre,
                    c.IdCategoriaProducto,
                    CategoriaProducto = c.IdCategoriaProductoNavigation.Nombre,
                    c.FechaRegistro,
                    c.UsuarioRegistro,
                    c.Estado
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/SubCatProducto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaProd>> GetSubCategoriaProd(int id)
        {
          if (_context.SubCategoriaProds == null)
          {
              return NotFound();
          }
            var subCategoriaProd = await _context.SubCategoriaProds.FindAsync(id);

            if (subCategoriaProd == null)
            {
                return NotFound();
            }

            return subCategoriaProd;
        }

        // PUT: api/SubCatProducto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoriaProd(int id, SubCatProductoViewModel req)
        {
            var subCat = await _context.SubCategoriaProds.FirstOrDefaultAsync(c => c.IdSubCatProd == id);

            if(subCat == null)
            {
                return NotFound();
            }

            subCat.IdCategoriaProducto = req.IdCategoriaProducto;
            subCat.Nombre = req.Nombre;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoriaProdExists(id))
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

        // POST: api/SubCatProducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategoriaProd>> PostSubCategoriaProd(SubCatProductoViewModel req)
        {
          if (_context.SubCategoriaProds == null)
          {
              return Problem("Entity set 'TokesContext.SubCategoriaProds'  is null.");
          }
            SubCategoriaProd subCategoriaProd = new SubCategoriaProd
            {
                IdCategoriaProducto = req.IdCategoriaProducto,
                Nombre = req.Nombre,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true
            };
            _context.SubCategoriaProds.Add(subCategoriaProd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoriaProd", new { id = subCategoriaProd.IdSubCatProd }, subCategoriaProd);
        }

        // DELETE: api/SubCatProducto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoriaProd(int id)
        {
            if (_context.SubCategoriaProds == null)
            {
                return NotFound();
            }
            var subCategoriaProd = await _context.SubCategoriaProds.FindAsync(id);
            if (subCategoriaProd == null)
            {
                return NotFound();
            }

            _context.SubCategoriaProds.Remove(subCategoriaProd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoriaProdExists(int id)
        {
            return (_context.SubCategoriaProds?.Any(e => e.IdSubCatProd == id)).GetValueOrDefault();
        }
    }
}
