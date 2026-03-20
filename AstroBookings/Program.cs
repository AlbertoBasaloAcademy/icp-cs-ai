using AstroBookings.Rockets;
using AstroBookings.Lanzamientos;

var builder = WebApplication.CreateBuilder(args);

// Configuración de logging de consola nativo
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IRepositorioCohete, RepositorioCoheteEnMemoria>();
builder.Services.AddSingleton<IRepositorioLanzamiento, RepositorioLanzamientoEnMemoria>();

var app = builder.Build();

// Middleware para registrar solicitudes, respuestas y errores
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
        .CreateLogger("RequestResponseLogger");

    var inicio = DateTimeOffset.UtcNow;
    var metodo = context.Request.Method;
    var ruta = context.Request.Path.Value ?? "/";

    logger.LogInformation(
        "Solicitud entrante {Metodo} {Ruta} a las {Inicio}",
        metodo,
        ruta,
        inicio);

    var sw = System.Diagnostics.Stopwatch.StartNew();

    try
    {
        await next.Invoke();
        sw.Stop();

        var estado = context.Response.StatusCode;
        var duracionMs = sw.ElapsedMilliseconds;

        if (estado >= 400 && estado < 600)
        {
            logger.LogWarning(
                "Respuesta {Metodo} {Ruta} con estado {Estado} en {Duracion} ms",
                metodo,
                ruta,
                estado,
                duracionMs);
        }
        else
        {
            logger.LogInformation(
                "Respuesta {Metodo} {Ruta} con estado {Estado} en {Duracion} ms",
                metodo,
                ruta,
                estado,
                duracionMs);
        }
    }
    catch (Exception ex)
    {
        sw.Stop();
        var duracionMs = sw.ElapsedMilliseconds;

        logger.LogError(
            ex,
            "Error no controlado procesando {Metodo} {Ruta} tras {Duracion} ms",
            metodo,
            ruta,
            duracionMs);

        if (!context.Response.HasStarted)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var problema = new
            {
                titulo = "Error interno en el servidor",
                detalle = "Se ha producido un error inesperado. Inténtelo de nuevo más tarde.",
                codigoEstado = context.Response.StatusCode,
                ruta
            };

            var json = System.Text.Json.JsonSerializer.Serialize(problema);
            await context.Response.WriteAsync(json);
        }
    }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new
{
    status = "ok",
    currentTime = DateTimeOffset.Now
}));

app.MapearEndpointsCohete();
app.MapearEndpointsLanzamiento();

app.Run();
