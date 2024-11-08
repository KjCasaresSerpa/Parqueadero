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
    public class MotosController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public MotosController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moto>>> ObtenerMotos()
        {
            return await _context.Motos.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Moto>> ObtenerMoto(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if(moto==null)
            {
                return Conflict("Hubo un problema en cargar su moto");
            }
            return moto;
        }

        [HttpPost]
        public async Task<ActionResult<Moto>> CrearMoto(Moto moto)
        {
            moto.HoraEntrada = DateTime.Now;
            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerMoto), new{id=moto.Id}, moto);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutMoto(int id, Moto moto)
    {
        if (id != moto.Id)
        {
            return BadRequest();
        }

        _context.Entry(moto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MotoExists(id))
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
        public async Task<IActionResult> DeleteMoto(int id)
        {
        var moto = await _context.Motos.FindAsync(id);
        if (ObtenerMotos == null)
        {
            return NotFound();
        }

        _context.Motos.Remove(moto);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool MotoExists(int id)
        {
        return _context.Motos.Any(e => e.Id == id);
        }
    }


}

