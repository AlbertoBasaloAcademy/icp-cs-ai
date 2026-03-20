namespace AstroBookings.Lanzamientos;

/// <summary>
/// Repositorio en memoria para gestionar lanzamientos durante la ejecución de la API.
/// </summary>
public sealed class RepositorioLanzamientoEnMemoria : IRepositorioLanzamiento
{
    private readonly Dictionary<int, Lanzamiento> lanzamientosPorId = new();
    private int siguienteId = 1;

    /// <summary>
    /// Obtiene todos los lanzamientos ordenados por identificador.
    /// </summary>
    /// <returns>Colección inmutable de lanzamientos.</returns>
    public IReadOnlyCollection<Lanzamiento> ObtenerTodos() =>
        lanzamientosPorId.Values.OrderBy(lanzamiento => lanzamiento.Id).ToArray();

    /// <summary>
    /// Crea un lanzamiento en estado programado.
    /// </summary>
    /// <param name="borrador">Datos base del lanzamiento.</param>
    /// <returns>Lanzamiento creado con identificador asignado.</returns>
    public Lanzamiento Crear(BorradorLanzamiento borrador)
    {
        var lanzamiento = new Lanzamiento(
            siguienteId++,
            borrador.CoheteId,
            borrador.FechaProgramada,
            EstadoLanzamiento.Programado,
            borrador.UmbralMinimoPasajeros,
            borrador.PasajerosConfirmados,
            borrador.Asientos.ToArray(),
            null);

        lanzamientosPorId[lanzamiento.Id] = lanzamiento;
        return lanzamiento;
    }

    /// <summary>
    /// Intenta obtener un lanzamiento por su identificador.
    /// </summary>
    /// <param name="id">Identificador del lanzamiento.</param>
    /// <param name="lanzamiento">Lanzamiento encontrado cuando existe.</param>
    /// <returns><see langword="true"/> si existe; de lo contrario <see langword="false"/>.</returns>
    public bool IntentarObtenerPorId(int id, out Lanzamiento lanzamiento) =>
        lanzamientosPorId.TryGetValue(id, out lanzamiento!);

    /// <summary>
    /// Actualiza los datos editables de un lanzamiento existente.
    /// </summary>
    /// <param name="id">Identificador del lanzamiento.</param>
    /// <param name="borrador">Nuevos valores del lanzamiento.</param>
    /// <param name="lanzamientoActualizado">Lanzamiento actualizado cuando existe.</param>
    /// <returns><see langword="true"/> si existe; de lo contrario <see langword="false"/>.</returns>
    public bool IntentarActualizar(int id, BorradorLanzamiento borrador, out Lanzamiento lanzamientoActualizado)
    {
        if (!lanzamientosPorId.TryGetValue(id, out var actual))
        {
            lanzamientoActualizado = default!;
            return false;
        }

        lanzamientoActualizado = actual with
        {
            CoheteId = borrador.CoheteId,
            FechaProgramada = borrador.FechaProgramada,
            UmbralMinimoPasajeros = borrador.UmbralMinimoPasajeros,
            PasajerosConfirmados = borrador.PasajerosConfirmados,
            Asientos = borrador.Asientos.ToArray()
        };

        lanzamientosPorId[id] = lanzamientoActualizado;
        return true;
    }

    /// <summary>
    /// Elimina un lanzamiento por su identificador.
    /// </summary>
    /// <param name="id">Identificador del lanzamiento.</param>
    /// <returns><see langword="true"/> si se elimina; de lo contrario <see langword="false"/>.</returns>
    public bool Eliminar(int id) => lanzamientosPorId.Remove(id);

    /// <summary>
    /// Intenta cambiar el estado de un lanzamiento aplicando reglas de transición.
    /// </summary>
    /// <param name="id">Identificador del lanzamiento.</param>
    /// <param name="estadoDestino">Nuevo estado solicitado.</param>
    /// <param name="motivo">Motivo del cambio cuando aplique.</param>
    /// <param name="lanzamientoActualizado">Lanzamiento actualizado cuando la transición es válida.</param>
    /// <param name="conflicto">Detalle del conflicto cuando la transición no aplica.</param>
    /// <returns><see langword="true"/> si se actualiza el estado; de lo contrario <see langword="false"/>.</returns>
    public bool IntentarCambiarEstado(
        int id,
        EstadoLanzamiento estadoDestino,
        string? motivo,
        out Lanzamiento lanzamientoActualizado,
        out string conflicto)
    {
        if (!lanzamientosPorId.TryGetValue(id, out var actual))
        {
            lanzamientoActualizado = default!;
            conflicto = string.Empty;
            return false;
        }

        if (!MaquinaEstadoLanzamiento.IntentarCambiar(actual, estadoDestino, motivo, out lanzamientoActualizado, out conflicto))
        {
            return false;
        }

        lanzamientosPorId[id] = lanzamientoActualizado;
        return true;
    }
}
