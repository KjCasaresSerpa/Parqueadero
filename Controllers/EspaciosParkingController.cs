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
    public class EspaciosParkingController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public EspaciosParkingController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspaciosParking>>> ObtenerEspaciosParkings()
        {
            return await _context.EspaciosParkings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspaciosParking>> ObtenerEspaciosParking(int id)
        {
            var espaciosparking = await _context.EspaciosParkings.FindAsync(id);
            if(espaciosparking==null)
            {
                return Conflict("ha ocurrido un error");
            }
            return espaciosparking;
        }

        [HttpPost]
        public async Task<ActionResult<EspaciosParking>> CrearEspaciosParking(EspaciosParking espaciosparking)
        {
           
            _context.EspaciosParkings.Add(espaciosparking);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerEspaciosParking), new{id=espaciosparking.Id}, espaciosparking);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutEspaciosParking(int id, EspaciosParking espaciosparking)
    {
        if (id != espaciosparking.Id)
        {
            return BadRequest();
        }

        _context.Entry(espaciosparking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EspaciosParkingExists(id))
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
        public async Task<IActionResult> DeleteEspaciosParking(int id)
        {
        var espaciosparking = await _context.EspaciosParkings.FindAsync(id);
        if (espaciosparking == null)
        {
            return NotFound();
        }

        _context.EspaciosParkings.Remove(espaciosparking);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool EspaciosParkingExists(int id)
        {
        return _context.EspaciosParkings.Any(e => e.Id == id);
        }
    }


}