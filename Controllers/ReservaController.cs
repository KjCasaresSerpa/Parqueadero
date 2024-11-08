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
    public class ReservaController : ControllerBase
    {
        private readonly ParqueaderoContext _context;
        public ReservaController(ParqueaderoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> Reservas()
        {
            return await _context.Reservas.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Reserva>> ObtenerReservas(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if(reserva==null)
            {
                return Conflict("ha ocurrido un error");
            }
            return reserva;
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> CrearReserva(Reserva reserva)
        {
            reserva.HoraEstimadaLlegada = DateTime.Now;
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ObtenerReservas), new{id=reserva.Id}, reserva);
        }

         [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
    {
        if (id != reserva.Id)
        {
            return BadRequest();
        }

        _context.Entry(reserva).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReservaExists(id))
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
        public async Task<IActionResult> DeleteReserva(int id)
        {
        var reserva = await _context.Reservas.FindAsync(id);
        if (ObtenerReservas == null)
        {
            return NotFound();
        }

        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();

        return NoContent();
        }

        private bool ReservaExists(int id)
        {
        return _context.Reservas.Any(e => e.Id == id);
        }
    }


}