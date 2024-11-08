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
    public class CarrosController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public CarrosController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carro>>> ObtenerCarros()
        {
            return await _context.Carros.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Carro>> ObtenerCarro(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if(carro==null)
            {
                return Conflict("Hubo un problema en cargar su Carro");
            }
            return carro;
        }

        [HttpPost]
        public async Task<ActionResult<Carro>> CrearCarro(Carro carro)
        {
            carro.HoraEntrada = DateTime.Now;
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerCarro), new{id=carro.Id}, carro);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutCarro(int id, Carro carro)
    {
        if (id != carro.Id)
        {
            return BadRequest();
        }

        _context.Entry(carro).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarroExists(id))
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
        public async Task<IActionResult> DeleteCarro(int id)
        {
        var carro = await _context.  Carros.FindAsync(id);
        if (ObtenerCarros == null)
        {
            return NotFound();
        }

        _context.Carros.Remove(carro);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool CarroExists(int id)
        {
        return _context.Carros.Any(e => e.Id == id);
        }
    }


}