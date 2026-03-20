namespace AstroBookings.Rockets;

/// <summary>
/// Repositorio en memoria para gestionar cohetes durante la ejecución de la API.
/// </summary>
public sealed class RepositorioCoheteEnMemoria : IRepositorioCohete
{
    private readonly Dictionary<int, Cohete> cohetesPorId = new();
    private int siguienteId = 1;

    /// <summary>
    /// Obtiene todos los cohetes ordenados por identificador.
    /// </summary>
    /// <returns>Colección inmutable con los cohetes existentes.</returns>
    public IReadOnlyCollection<Cohete> ObtenerTodos() =>
        cohetesPorId.Values.OrderBy(cohete => cohete.Id).ToArray();

    /// <summary>
    /// Crea y guarda un nuevo cohete a partir de un borrador.
    /// </summary>
    /// <param name="borrador">Datos necesarios para crear el cohete.</param>
    /// <returns>Cohete creado con identificador asignado.</returns>
    public Cohete Crear(BorradorCohete borrador)
    {
        var cohete = CrearCohete(siguienteId++, borrador);
        cohetesPorId[cohete.Id] = cohete;
        return cohete;
    }

    /// <summary>
    /// Intenta obtener un cohete por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cohete.</param>
    /// <param name="cohete">Cohete encontrado cuando la operación tiene éxito.</param>
    /// <returns><see langword="true"/> si existe; en otro caso, <see langword="false"/>.</returns>
    public bool IntentarObtenerPorId(int id, out Cohete cohete) =>
        cohetesPorId.TryGetValue(id, out cohete!);

    /// <summary>
    /// Intenta actualizar un cohete existente.
    /// </summary>
    /// <param name="id">Identificador del cohete a actualizar.</param>
    /// <param name="borrador">Nuevos datos del cohete.</param>
    /// <param name="coheteActualizado">Cohete actualizado cuando la operación tiene éxito.</param>
    /// <returns><see langword="true"/> si el cohete existe; en otro caso, <see langword="false"/>.</returns>
    public bool IntentarActualizar(int id, BorradorCohete borrador, out Cohete coheteActualizado)
    {
        if (!cohetesPorId.ContainsKey(id))
        {
            coheteActualizado = default!;
            return false;
        }

        coheteActualizado = CrearCohete(id, borrador);
        cohetesPorId[id] = coheteActualizado;
        return true;
    }

    /// <summary>
    /// Elimina un cohete por su identificador.
    /// </summary>
    /// <param name="id">Identificador del cohete.</param>
    /// <returns><see langword="true"/> si se elimina; en otro caso, <see langword="false"/>.</returns>
    public bool Eliminar(int id) => cohetesPorId.Remove(id);

    // Centraliza la creación para mantener consistencia entre alta y actualización.
    private static Cohete CrearCohete(int id, BorradorCohete borrador) =>
        new(id, borrador.Nombre, borrador.Alcance, borrador.Capacidad);
}
