
using Microsoft.EntityFrameworkCore;

public class ParqueaderoContext : DbContext 
{
    public ParqueaderoContext (DbContextOptions<ParqueaderoContext> options):base(options)
    {

    }

    public DbSet<Bicicleta> Bicicletas {get ; set ;}
    public DbSet<Carro> Carros {get ; set ;}
    public DbSet<Moto> Motos {get ; set ;}
    public DbSet<EspaciosParking> EspaciosParkings {get ; set ;}
    public DbSet<Reserva> Reservas {get ; set ;}
    public DbSet<Tarifa> Tarifas {get ; set ;}

}