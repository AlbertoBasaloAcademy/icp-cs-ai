namespace AstroBookings.Rockets;

public interface IRepositorioCohete
{
    IReadOnlyCollection<Cohete> ObtenerTodos();
    Cohete Crear(BorradorCohete cohete);
    bool IntentarObtenerPorId(int id, out Cohete cohete);
    bool IntentarActualizar(int id, BorradorCohete cohete, out Cohete coheteActualizado);
    bool Eliminar(int id);
}
