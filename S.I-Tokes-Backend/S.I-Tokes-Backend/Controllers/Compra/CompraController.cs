using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Compra.Compra;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Compra
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly TokesContext _context;

        public CompraController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/Compra
        [HttpGet]
        public async Task<IActionResult> GetCompras()
        {
          if (_context.Compras == null)
          {
              return NotFound();
          }

          var result = await _context.Compras
                .Select(c => new
                {
                    c.IdCompra,
                    c.NoOrden,
                    c.IdProveedor,
                    Proveedor = c.IdProveedorNavigation.Nombre,
                    c.Aprobada,
                    c.Observaciones,
                    c.FechaRegistro,
                    c.UsuarioRegistro,
                    c.Estado,
                    Total = c.DetalleCompras.Sum(c => c.Cantidad * c.CostoUnitario)
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Compra/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tokes.Entidades.Compra>> GetCompra(int id)
        {
          if (_context.Compras == null)
          {
              return NotFound();
          }
            var compra = await _context.Compras
                .Include(c => c.DetalleCompras)
                .FirstOrDefaultAsync(c => c.IdCompra == id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }

        // PUT: api/Compra/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra(int id, CompraViewModel req)
        {
            var compra = await _context.Compras.Include(c => c.DetalleCompras).FirstOrDefaultAsync(c => c.IdCompra == id);

            if(compra == null)
            {
                return NotFound();
            }

            compra.NoOrden = req.NoOrden;
            compra.IdProveedor = req.IdProveedor;
            compra.Aprobada = req.Aprobada;
            compra.Observaciones = req.Observaciones;

            compra.DetalleCompras.Clear();
            foreach (var detalleDto in req.Detalle)
            {
                compra.DetalleCompras.Add(new Tokes.Entidades.DetalleCompra
                {
                    IdCompra = detalleDto.IdCompra,
                    IdProducto = detalleDto.IdProducto,
                    Cantidad = detalleDto.Cantidad,
                    CostoUnitario = detalleDto.CostoUnitario,
                    Observaciones = detalleDto.Observaciones
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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

        // POST: api/Compra
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tokes.Entidades.Compra>> PostCompra(CompraViewModel req)
        {
          if (_context.Compras == null)
          {
              return Problem("Entity set 'TokesContext.Compras'  is null.");
          }

            Tokes.Entidades.Compra compra = new Tokes.Entidades.Compra
            {
                NoOrden = req.NoOrden,
                IdProveedor = req.IdProveedor,
                Aprobada = req.Aprobada,
                Observaciones = req.Observaciones,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = req.UsuarioRegistro,
                Estado = true,
                DetalleCompras = req.Detalle.Select(d => new Tokes.Entidades.DetalleCompra
                {
                    IdCompra = d.IdCompra,
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    CostoUnitario = d.CostoUnitario,
                    Observaciones = d.Observaciones
                }).ToList()
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompra", new { id = compra.IdCompra }, compra);
        }

        // DELETE: api/Compra/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            if (_context.Compras == null)
            {
                return NotFound();
            }

            var detalles = await _context.DetalleCompras.Where(c => c.IdCompra == id).ToListAsync();

            _context.DetalleCompras.RemoveRange(detalles);

            await _context.SaveChangesAsync();

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return (_context.Compras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }
    }
}
