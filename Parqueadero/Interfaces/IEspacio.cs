public interface IEspacio
{
    int EspaciosDisponibles { get; }
    bool AsignarEspacio(Vehiculo vehiculo);
    bool LiberarEspacio(string placa);   
}