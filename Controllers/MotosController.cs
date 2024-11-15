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

        [HttpGet("{id}")]
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
         public async Task<ActionResult<Moto>> CrearBicicleta (Moto moto)
        {
            moto.HoraEntrada = DateTime.Now;
            _context.Motos.Add(moto);
            var espacioM = await _context.EspaciosParkings.FirstOrDefaultAsync(e => e.Tipo == "Moto"); 

            if (espacioM == null)
            {
                return Conflict("Hubo un problema al cargar los espacios para 'Moto'");
            }
            if (espacioM.CantidadEspacios == 0)
            {
                return Conflict("No hay espacios disponibles para el tipo de vehiculo");
            }



            espacioM.CantidadEspacios -= 1;

            TimeSpan tiempo;

            if (moto.HoraSalida.HasValue && moto.HoraEntrada.HasValue)
            {
                tiempo = moto.HoraSalida.Value - moto.HoraEntrada.Value;
                var valor = await _context.Tarifas.FirstOrDefaultAsync(t => t.TipoVehiculo == "Moto");
                decimal valorHoras = valor.CostoPorHora * tiempo.Hours;
                moto.TotalAPAgar = valorHoras;
            }
            else
            {
                Conflict("Hubo un problema al calcular el tiempo de permanencia");
            }


            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerMoto), new { id = moto.Id }, moto);
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
        if (moto == null)
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

         [HttpPatch("salida/{id}")]
        public async Task<IActionResult> SalidaAutomatica(int id)
        {
            var moto = await _context.Motos.FindAsync(id);

            DateTime salida = DateTime.Now;

            moto.HoraSalida = salida;


            TimeSpan tiempo;

            if (moto.HoraSalida.HasValue && moto.HoraEntrada.HasValue)
            {
                tiempo = moto.HoraSalida.Value - moto.HoraEntrada.Value;
                var valor = await _context.Tarifas.FirstOrDefaultAsync(t => t.TipoVehiculo == "Moto");
                decimal valorHoras = valor.CostoPorHora * tiempo.Hours;
                moto.TotalAPAgar = valorHoras;
            }
            else
            {
                return Conflict("Hubo un problema al calcular el tiempo de permanencia");
            }

            await _context.SaveChangesAsync();
            
            return NoContent();
    }

    }
}

