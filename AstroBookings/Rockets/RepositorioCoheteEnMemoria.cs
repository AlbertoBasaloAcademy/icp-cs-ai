namespace AstroBookings.Rockets;

public sealed class RepositorioCoheteEnMemoria : IRepositorioCohete
{
    private readonly Dictionary<int, Cohete> cohetes = new();
    private int siguienteId = 1;

    public IReadOnlyCollection<Cohete> ObtenerTodos() => cohetes.Values.OrderBy(cohete => cohete.Id).ToArray();

    public Cohete Crear(BorradorCohete cohete)
    {
        var coheteCreado = new Cohete(siguienteId++, cohete.Nombre, cohete.Alcance, cohete.Capacidad);
        cohetes[coheteCreado.Id] = coheteCreado;
        return coheteCreado;
    }

    public bool IntentarObtenerPorId(int id, out Cohete cohete)
    {
        if (cohetes.TryGetValue(id, out var coheteExistente))
        {
            cohete = coheteExistente;
            return true;
        }

        cohete = default!;
        return false;
    }

    public bool IntentarActualizar(int id, BorradorCohete cohete, out Cohete coheteActualizado)
    {
        if (!cohetes.ContainsKey(id))
        {
            coheteActualizado = default!;
            return false;
        }

        coheteActualizado = new Cohete(id, cohete.Nombre, cohete.Alcance, cohete.Capacidad);
        cohetes[id] = coheteActualizado;
        return true;
    }

    public bool Eliminar(int id) => cohetes.Remove(id);
}
