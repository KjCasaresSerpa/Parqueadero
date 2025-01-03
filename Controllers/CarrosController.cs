using System;
using System.Collections.Generic;
using System.Data;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> ObtenerCarro(int id)
        {
            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
            {
                return Conflict("Hubo un problema en cargar su Carro");
            }
            return carro;
        }
        [HttpGet("reporte")]
        public async Task<ActionResult<IEnumerable<Carro>>> ReporteCarros()
        {
            var carros = await _context.Carros.Where(c => DateTime.UtcNow <= c.HoraSalida ).ToListAsync();
            return carros;
        }

        [HttpPost]
        public async Task<ActionResult<Carro>> CrearCarro(Carro carro)
        {
            carro.HoraEntrada = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            _context.Carros.Add(carro);
            var espacio = await _context.EspaciosParkings.FirstOrDefaultAsync(e => e.Tipo == "Carro"); // Esta linea busca en la tabla ese registro de tipo carro

            if (espacio == null)
            {
                return Conflict("Hubo un problema al cargar los espacios para 'Carro'");
            }
            if (espacio.CantidadEspacios == 0)
            {
                return Conflict("No hay espacios disponibles para el tipo de vehiculo");
            }

            espacio.CantidadEspacios -= 1;

            TimeSpan tiempo;

            if (carro.HoraSalida.HasValue && carro.HoraEntrada.HasValue)
            {
                
                tiempo = carro.HoraEntrada.Value - carro.HoraSalida.Value;
                var valor = await _context.Tarifas.FirstOrDefaultAsync(t => t.TipoVehiculo == "Carro");
                decimal minutos_A_Horas = Math.Abs((decimal)tiempo.Minutes / 60);

                decimal descuento = calcularDescuentos(carro);

                decimal ValorMinutos = Math.Ceiling(descuento > 0 ? descuento : valor.CostoPorHora * minutos_A_Horas / 100) * 100;
                decimal ValorHoras = descuento > 0 ? descuento : valor.CostoPorHora * tiempo.Hours;
                carro.TotalAPAgar = ValorMinutos + ValorHoras;
            }
            else
            {
                return Conflict("Hubo un problema al calcular el tiempo de permanencia");
            }


            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerCarro), new { id = carro.Id }, carro);
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
            var carro = await _context.Carros.FindAsync(id);
            if (carro == null)
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


        [HttpPatch("salida/{id}")]
        public async Task<IActionResult> SalidaAutomatica(int id)
        {
            var carro = await _context.Carros.FindAsync(id);

            DateTime salida = DateTime.Now;

            carro.HoraSalida = salida;


            TimeSpan tiempo;

            if (carro.HoraSalida.HasValue && carro.HoraEntrada.HasValue)
            {
                tiempo = carro.HoraSalida.Value - carro.HoraEntrada.Value;
                var valor = await _context.Tarifas.FirstOrDefaultAsync(t => t.TipoVehiculo == "Carro");
                decimal valorHoras = valor.CostoPorHora * tiempo.Hours;
                carro.TotalAPAgar = valorHoras;
            }
            else
            {
                return Conflict("Hubo un problema al calcular el tiempo de permanencia");
            }

            await _context.SaveChangesAsync();

            return NoContent();


        }

        private decimal calcularDescuentos(Carro carro)
        {
            var tarifa = _context.Tarifas.FirstOrDefault(t => t.TipoVehiculo == "Carro");
            int visitas = _context.Carros.Where(v => v.Placa == carro.Placa).Count();

            if (tarifa == null)
            {
                return 0;
            }

            if (visitas > 1)
            {
                decimal descuento = tarifa.CostoPorHora * tarifa.Descuento;
                return descuento;
            }

            return 0;


        }

    }

}