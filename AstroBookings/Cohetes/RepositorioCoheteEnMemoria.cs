namespace AstroBookings.Rockets;

public sealed class RepositorioCoheteEnMemoria : IRepositorioCohete
{
    private readonly Dictionary<int, Cohete> cohetes = new();
    private int siguienteId = 1;

    public IReadOnlyCollection<Cohete> ObtenerTodos() => cohetes.Values.OrderBy(cohete => cohete.Id).ToArray();

    public Cohete Crear(BorradorCohete borrador)
    {
        var cohete = new Cohete(siguienteId++, borrador.Nombre, borrador.Alcance, borrador.Capacidad);
        cohetes[cohete.Id] = cohete;
        return cohete;
    }

    public bool IntentarObtenerPorId(int id, out Cohete cohete) =>
        cohetes.TryGetValue(id, out cohete!);

    public bool IntentarActualizar(int id, BorradorCohete borrador, out Cohete coheteActualizado)
    {
        if (!cohetes.ContainsKey(id))
        {
            coheteActualizado = default!;
            return false;
        }

        coheteActualizado = new Cohete(id, borrador.Nombre, borrador.Alcance, borrador.Capacidad);
        cohetes[id] = coheteActualizado;
        return true;
    }

    public bool Eliminar(int id) => cohetes.Remove(id);
}
