namespace AstroBookings.Rockets;

/// <summary>
/// Configura y registra los endpoints HTTP para la gestión de cohetes.
/// </summary>
public static class EndpointsCohete
{
    /// <summary>
    /// Mapea los endpoints CRUD del recurso <c>/cohetes</c> en el grupo de rutas.
    /// </summary>
    /// <param name="endpoints">El generador de rutas de la aplicación.</param>
    /// <returns>El mismo <see cref="IEndpointRouteBuilder"/> para encadenamiento fluido.</returns>
    /// <example>
    /// <code>
    /// app.MapearEndpointsCohete();
    /// </code>
    /// </example>
    public static IEndpointRouteBuilder MapearEndpointsCohete(this IEndpointRouteBuilder endpoints)
    {
        var grupo = endpoints.MapGroup("/cohetes");

        grupo.MapGet("/", ObtenerTodos);
        grupo.MapGet("/{id:int}", ObtenerPorId);
        grupo.MapPost("/", Crear);
        grupo.MapPut("/{id:int}", Actualizar);
        grupo.MapDelete("/{id:int}", Eliminar);

        return endpoints;
    }

    // Devuelve la lista completa de cohetes como DTOs de respuesta.
    private static IResult ObtenerTodos(IRepositorioCohete repositorio) =>
        Results.Ok(repositorio.ObtenerTodos().Select(cohete => cohete.ARespuesta()));

    // Devuelve un cohete por su identificador o 404 si no existe.
    private static IResult ObtenerPorId(int id, IRepositorioCohete repositorio) =>
        repositorio.IntentarObtenerPorId(id, out var cohete)
            ? Results.Ok(cohete.ARespuesta())
            : Results.NotFound();

    // Valida la solicitud y crea un nuevo cohete; retorna 400 si la validación falla.
    private static IResult Crear(SolicitudCohete solicitud, IRepositorioCohete repositorio)
    {
        if (!ValidacionCohete.IntentarValidar(solicitud, out var cohete, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        var coheteCreado = repositorio.Crear(cohete);

        return Results.Created($"/cohetes/{coheteCreado.Id}", coheteCreado.ARespuesta());
    }

    // Valida la solicitud y actualiza el cohete indicado; retorna 404 si no existe.
    private static IResult Actualizar(int id, SolicitudCohete solicitud, IRepositorioCohete repositorio)
    {
        if (!ValidacionCohete.IntentarValidar(solicitud, out var cohete, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        return repositorio.IntentarActualizar(id, cohete, out var coheteActualizado)
            ? Results.Ok(coheteActualizado.ARespuesta())
            : Results.NotFound();
    }

    // Elimina el cohete indicado o retorna 404 si no existe.
    private static IResult Eliminar(int id, IRepositorioCohete repositorio) =>
        repositorio.Eliminar(id)
            ? Results.NoContent()
            : Results.NotFound();
}
