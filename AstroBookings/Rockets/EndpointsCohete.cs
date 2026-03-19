namespace AstroBookings.Rockets;

public static class EndpointsCohete
{
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

    private static IResult ObtenerTodos(IRepositorioCohete repositorio) =>
        Results.Ok(repositorio.ObtenerTodos().Select(cohete => cohete.ARespuesta()));

    private static IResult ObtenerPorId(int id, IRepositorioCohete repositorio) =>
        repositorio.IntentarObtenerPorId(id, out var cohete)
            ? Results.Ok(cohete.ARespuesta())
            : Results.NotFound();

    private static IResult Crear(SolicitudCohete solicitud, IRepositorioCohete repositorio)
    {
        if (!ValidacionCohete.IntentarValidar(solicitud, out var cohete, out var errores))
        {
            return Results.ValidationProblem(errores);
        }

        var coheteCreado = repositorio.Crear(cohete);
        return Results.Created($"/cohetes/{coheteCreado.Id}", coheteCreado.ARespuesta());
    }

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

    private static IResult Eliminar(int id, IRepositorioCohete repositorio) =>
        repositorio.Eliminar(id)
            ? Results.NoContent()
            : Results.NotFound();
}
