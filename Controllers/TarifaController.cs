using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Parqueadero.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public TarifaController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarifa>>> Tarifas()
        {
            return await _context.Tarifas.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Tarifa>> ObtenerTarifas(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if(tarifa==null)
            {
                return Conflict("ha ocurrido un error");
            }
            return tarifa;
        }

        [HttpPost]
        public async Task<ActionResult<Tarifa>> CrearTarifa(Tarifa tarifa)
        {
            
            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerTarifas), new{id=tarifa.Id}, tarifa);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutTarifa(int id, Tarifa tarifa)
    {
        if (id != tarifa.Id)
        {
            return BadRequest();
        }

        _context.Entry(tarifa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TarifaExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarifa(int id)
        {
        var tarifa = await _context.Tarifas.FindAsync(id);
        if (ObtenerTarifas == null)
        {
            return NotFound();
        }

        _context.Tarifas.Remove(tarifa);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool TarifaExists(int id)
        {
        return _context.Tarifas.Any(e => e.Id == id);
        }
    }


}