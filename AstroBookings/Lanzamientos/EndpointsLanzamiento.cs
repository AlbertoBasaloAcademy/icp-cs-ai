using AstroBookings.Rockets;

namespace AstroBookings.Lanzamientos;

/// <summary>
/// Configura y registra los endpoints HTTP para la gestión de lanzamientos.
/// </summary>
public static class EndpointsLanzamiento
{
    /// <summary>
    /// Mapea los endpoints CRUD y de transición de estado del recurso <c>/lanzamientos</c>.
    /// </summary>
    /// <param name="endpoints">El generador de rutas de la aplicación.</param>
    /// <returns>El mismo <see cref="IEndpointRouteBuilder"/> para encadenamiento fluido.</returns>
    public static IEndpointRouteBuilder MapearEndpointsLanzamiento(this IEndpointRouteBuilder endpoints)
    {
        var grupo = endpoints.MapGroup("/lanzamientos");

        grupo.MapGet("/", ObtenerTodos);
        grupo.MapGet("/{id:int}", ObtenerPorId);
        grupo.MapPost("/", Crear);
        grupo.MapPut("/{id:int}", Actualizar);
        grupo.MapDelete("/{id:int}", Eliminar);
        grupo.MapPatch("/{id:int}/estado", CambiarEstado);

        return endpoints;
    }

    // Devuelve la lista completa de lanzamientos como DTOs de respuesta.
    private static IResult ObtenerTodos(IRepositorioLanzamiento repositorio) =>
        Results.Ok(repositorio.ObtenerTodos().Select(lanzamiento => lanzamiento.ARespuesta()));

    // Devuelve un lanzamiento por su identificador o 404 si no existe.
    private static IResult ObtenerPorId(int id, IRepositorioLanzamiento repositorio) =>
        repositorio.IntentarObtenerPorId(id, out var lanzamiento)
            ? Results.Ok(lanzamiento.ARespuesta())
            : Results.NotFound();

    // Valida la solicitud y crea un lanzamiento en estado programado.
    private static IResult Crear(
        SolicitudLanzamiento solicitud,
        IRepositorioLanzamiento repositorioLanzamiento,
        IRepositorioCohete repositorioCohete)
    {
        if (!ValidacionLanzamiento.IntentarValidarSolicitud(solicitud, out var borrador, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        if (!repositorioCohete.IntentarObtenerPorId(borrador.CoheteId, out _))
        {
            return Results.NotFound(new { mensaje = "No existe el cohete indicado para el lanzamiento." });
        }

        var lanzamientoCreado = repositorioLanzamiento.Crear(borrador);
        return Results.Created($"/lanzamientos/{lanzamientoCreado.Id}", lanzamientoCreado.ARespuesta());
    }

    // Valida la solicitud y actualiza un lanzamiento existente.
    private static IResult Actualizar(
        int id,
        SolicitudLanzamiento solicitud,
        IRepositorioLanzamiento repositorioLanzamiento,
        IRepositorioCohete repositorioCohete)
    {
        if (!ValidacionLanzamiento.IntentarValidarSolicitud(solicitud, out var borrador, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        if (!repositorioCohete.IntentarObtenerPorId(borrador.CoheteId, out _))
        {
            return Results.NotFound(new { mensaje = "No existe el cohete indicado para el lanzamiento." });
        }

        return repositorioLanzamiento.IntentarActualizar(id, borrador, out var lanzamientoActualizado)
            ? Results.Ok(lanzamientoActualizado.ARespuesta())
            : Results.NotFound();
    }

    // Elimina un lanzamiento por id o devuelve 404 si no existe.
    private static IResult Eliminar(int id, IRepositorioLanzamiento repositorio) =>
        repositorio.Eliminar(id)
            ? Results.NoContent()
            : Results.NotFound();

    // Cambia el estado de un lanzamiento aplicando reglas de transición.
    private static IResult CambiarEstado(
        int id,
        SolicitudCambioEstadoLanzamiento solicitud,
        IRepositorioLanzamiento repositorio)
    {
        if (!ValidacionLanzamiento.IntentarValidarCambioEstado(solicitud, out var estadoDestino, out var motivo, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        if (!repositorio.IntentarObtenerPorId(id, out _))
        {
            return Results.NotFound();
        }

        return repositorio.IntentarCambiarEstado(id, estadoDestino, motivo, out var lanzamientoActualizado, out var conflicto)
            ? Results.Ok(lanzamientoActualizado.ARespuesta())
            : Results.Conflict(new { mensaje = conflicto });
    }
}
