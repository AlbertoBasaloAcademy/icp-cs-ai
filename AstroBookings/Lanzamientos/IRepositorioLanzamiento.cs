namespace AstroBookings.Lanzamientos;

public interface IRepositorioLanzamiento
{
    IReadOnlyCollection<Lanzamiento> ObtenerTodos();
    Lanzamiento Crear(BorradorLanzamiento borrador);
    bool IntentarObtenerPorId(int id, out Lanzamiento lanzamiento);
    bool IntentarActualizar(int id, BorradorLanzamiento borrador, out Lanzamiento lanzamientoActualizado);
    bool Eliminar(int id);
    bool IntentarCambiarEstado(int id, EstadoLanzamiento estadoDestino, string? motivo, out Lanzamiento lanzamientoActualizado, out string conflicto);
}
