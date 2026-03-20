namespace AstroBookings.Lanzamientos;

public enum EstadoLanzamiento
{
    Programado,
    Confirmado,
    Exitoso,
    Suspendido,
    Cancelado
}

public sealed record AsientoLanzamiento(string Tipo, decimal Precio);

public sealed record Lanzamiento(
    int Id,
    int CoheteId,
    DateTimeOffset FechaProgramada,
    EstadoLanzamiento Estado,
    int UmbralMinimoPasajeros,
    int PasajerosConfirmados,
    IReadOnlyList<AsientoLanzamiento> Asientos,
    string? MotivoEstado);

public sealed record BorradorLanzamiento(
    int CoheteId,
    DateTimeOffset FechaProgramada,
    int UmbralMinimoPasajeros,
    int PasajerosConfirmados,
    IReadOnlyList<AsientoLanzamiento> Asientos);

public sealed record SolicitudAsientoLanzamiento(string? Tipo, decimal Precio);

public sealed record SolicitudLanzamiento(
    int CoheteId,
    DateTimeOffset FechaProgramada,
    int UmbralMinimoPasajeros,
    int PasajerosConfirmados,
    IReadOnlyList<SolicitudAsientoLanzamiento>? Asientos);

public sealed record SolicitudCambioEstadoLanzamiento(string? Estado, string? Motivo);

public sealed record RespuestaAsientoLanzamiento(string Tipo, decimal Precio);

public sealed record RespuestaLanzamiento(
    int Id,
    int CoheteId,
    DateTimeOffset FechaProgramada,
    string Estado,
    int UmbralMinimoPasajeros,
    int PasajerosConfirmados,
    IReadOnlyList<RespuestaAsientoLanzamiento> Asientos,
    string? MotivoEstado);
