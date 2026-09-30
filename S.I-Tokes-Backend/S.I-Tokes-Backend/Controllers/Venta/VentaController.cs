using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S.I_Tokes_Backend.Models.Venta.Venta;
using Tokes.Datos;
using Tokes.Entidades;

namespace S.I_Tokes_Backend.Controllers.Venta
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly TokesContext _context;

        public VentaController(TokesContext context)
        {
            _context = context;
        }

        // GET: api/Venta
        [HttpGet]
        public async Task<IActionResult> GetVenta()
        {
          if (_context.Venta == null)
          {
              return NotFound();
          }

          var result = await _context.Venta
                .Select(c => new
                {
                    c.IdVenta,
                    c.NoVenta,
                    c.IdCliente,
                    Cliente = c.IdClienteNavigation.Codigo,
                    c.Credito,
                    c.Observaciones,
                    c.EnviarA,
                    c.FechaRegistro,
                    c.UsuarioRegistro,
                    c.Estado,
                    Total = c.DetalleVenta.Sum(c => c.Cantidad * c.PrecioUnitario)
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Venta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tokes.Entidades.Venta>> GetVenta(int id)
        {
          if (_context.Venta == null)
          {
              return NotFound();
          }
            var venta = await _context.Venta.Include(c => c.DetalleVenta).FirstOrDefaultAsync(c => c.IdVenta == id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // PUT: api/Venta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta(int id, VentaViewModel req)
        {
            var venta = await _context.Venta.Include(c => c.DetalleVenta).FirstOrDefaultAsync(c => c.IdVenta == id);

            if(venta == null)
            {
                return NotFound();
            }

            venta.NoVenta = req.NoVenta;
            venta.IdCliente = req.IdCliente;
            venta.Credito = req.Credito;
            venta.Observaciones = req.Observaciones;
            venta.EnviarA = req.EnviarA;

            venta.DetalleVenta.Clear();
            foreach (var detalle in req.DetalleVenta)
            {
                venta.DetalleVenta.Add(new Tokes.Entidades.DetalleVenta
                {
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Observaciones = detalle.Observaciones
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
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

        // POST: api/Venta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tokes.Entidades.Venta>> PostVenta(VentaViewModel req)
        {
          if (_context.Venta == null)
          {
              return Problem("Entity set 'TokesContext.Venta'  is null.");
          }

            Tokes.Entidades.Venta venta = new Tokes.Entidades.Venta
            {
                NoVenta = req.NoVenta,
                IdCliente = req.IdCliente,
                Credito = req.Credito,
                Observaciones = req.Observaciones,
                EnviarA = req.EnviarA,
                UsuarioRegistro = req.UsuarioRegistro,
                FechaRegistro = DateTime.Now,
                Estado = true,
                DetalleVenta = req.DetalleVenta.Select(d => new Tokes.Entidades.DetalleVenta
                {
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Observaciones = d.Observaciones
                }).ToList()
            };

            _context.Venta.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenta", new { id = venta.IdVenta }, venta);
        }

        // DELETE: api/Venta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            if (_context.Venta == null)
            {
                return NotFound();
            }

            var detalles = await _context.DetalleVenta.Where(c => c.IdVenta == id).ToListAsync();

            _context.DetalleVenta.RemoveRange(detalles);

            await _context.SaveChangesAsync();

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Venta.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaExists(int id)
        {
            return (_context.Venta?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
