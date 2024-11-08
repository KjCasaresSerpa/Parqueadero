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
    public class BicicletasController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public BicicletasController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicicleta>>> ObtenerBicicletas()
        {
            return await _context.Bicicletas.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Bicicleta>> ObtenerBicicleta(int id)
        {
            var bicicleta = await _context.Bicicletas.FindAsync(id);
            if(bicicleta==null)
            {
                return Conflict("Hubo un problema en cargar su bicicleta");
            }
            return bicicleta;
        }

        [HttpPost]
        public async Task<ActionResult<Bicicleta>> CrearBicicleta(Bicicleta bicicleta)
        {
            bicicleta.HoraEntrada = DateTime.Now;
            _context.Bicicletas.Add(bicicleta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerBicicleta), new{id=bicicleta.Id}, bicicleta);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutBicicleta(int id, Bicicleta bicicleta)
    {
        if (id != bicicleta.Id)
        {
            return BadRequest();
        }

        _context.Entry(bicicleta).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BicicletaExists(id))
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
        public async Task<IActionResult> DeleteBicicleta(int id)
        {
        var bicicleta = await _context.Bicicletas.FindAsync(id);
        if (ObtenerBicicletas == null)
        {
            return NotFound();
        }

        _context.Bicicletas.Remove(bicicleta);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool BicicletaExists(int id)
        {
        return _context.Bicicletas.Any(e => e.Id == id);
        }
    }


}