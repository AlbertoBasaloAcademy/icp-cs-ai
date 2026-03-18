namespace AstroBookings.Rockets;

public static class RocketEndpoints
{
    public static IEndpointRouteBuilder MapRocketEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/rockets");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:int}", Update);
        group.MapDelete("/{id:int}", Delete);

        return endpoints;
    }

    private static IResult GetAll(IRocketRepository repository) =>
        Results.Ok(repository.GetAll().Select(rocket => rocket.ToResponse()));

    private static IResult GetById(int id, IRocketRepository repository) =>
        repository.TryGetById(id, out var rocket)
            ? Results.Ok(rocket.ToResponse())
            : Results.NotFound();

    private static IResult Create(RocketRequest request, IRocketRepository repository)
    {
        if (!RocketValidation.TryValidate(request, out var rocket, out var errors))
        {
            return Results.ValidationProblem(errors);
        }

        var createdRocket = repository.Create(rocket);
        return Results.Created($"/rockets/{createdRocket.Id}", createdRocket.ToResponse());
    }

    private static IResult Update(int id, RocketRequest request, IRocketRepository repository)
    {
        if (!RocketValidation.TryValidate(request, out var rocket, out var errors))
        {
            return Results.ValidationProblem(errors);
        }

        return repository.TryUpdate(id, rocket, out var updatedRocket)
            ? Results.Ok(updatedRocket.ToResponse())
            : Results.NotFound();
    }

    private static IResult Delete(int id, IRocketRepository repository) =>
        repository.Delete(id)
            ? Results.NoContent()
            : Results.NotFound();
}
