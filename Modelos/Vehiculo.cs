public abstract class Vehiculo
{
    public string Placa { get; set; }
    public DateTime? HoraEntrada { get; set; }
    public DateTime? HoraSalida { get; set; }
    public decimal? TotalAPAgar { get; set; }
}