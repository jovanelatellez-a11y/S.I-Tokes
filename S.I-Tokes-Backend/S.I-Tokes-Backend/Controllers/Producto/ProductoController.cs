using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Producto.Producto;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Producto
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly TokesContext _context;

        public ProductoController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tokes.Entidades.Producto>>> GetProductos()
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            return await _context.Productos
                .Include(c => c.IdSubCatProdNavigation)
                .Include(c => c.IdUnidadMedidaNavigation)
                .ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tokes.Entidades.Producto>> GetProducto(int id)
        {
          if (_context.Productos == null)
          {
              return NotFound();
          }
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoViewModel req)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(c => c.IdProducto == id);

            if(producto == null)
            {
                return NotFound();
            }

            producto.IdSubCatProd = req.IdSubCatProd;
            producto.IdUnidadMedida = req.IdUnidadMedida;
            producto.Codigo = req.Codigo;
            producto.Nombre = req.Nombre;
            producto.Precio = req.Precio;
            producto.Costo = req.Costo;
            producto.CantidadTotal = req.CantidadTotal;
            producto.CantidadMinima = req.CantidadMinima;
            producto.Imagen = req.Imagen;
            producto.Observaciones = req.Observaciones;
            producto.TipoProducto = req.TipoProducto;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tokes.Entidades.Producto>> PostProducto(ProductoViewModel req)
        {
          if (_context.Productos == null)
          {
              return Problem("Entity set 'TokesContext.Productos'  is null.");
          }

            Tokes.Entidades.Producto producto = new Tokes.Entidades.Producto
            {
                IdSubCatProd = req.IdSubCatProd,
                IdUnidadMedida = req.IdUnidadMedida,
                Codigo = req.Codigo,
                Nombre = req.Nombre,
                Precio = req.Precio,
                Costo = req.Costo,
                CantidadTotal = req.CantidadTotal,
                CantidadMinima = req.CantidadMinima,
                Imagen = req.Imagen,
                Observaciones = req.Observaciones,
                TipoProducto = req.TipoProducto,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true                
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.IdProducto }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (_context.Productos == null)
            {
                return NotFound();
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }
    }
}
